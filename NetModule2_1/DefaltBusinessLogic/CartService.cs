using NetModule2_1.BAL;
using NetModule2_1.DAL;

namespace NetModule2_1.DefaultBusinessLogic
{
    internal class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public void AddItem(string id, BAL.Item item)
        {
            ValidateItem(item);
            var cart = cartRepository.LoadCart(id);
            cart ??= new Cart() { Id = id, Items = new List<DAL.Item>() };
            cart.Items.Add(Mapping.Map<BAL.Item, DAL.Item>(item));
            cartRepository.SaveCart(cart);
        }

        public List<BAL.Item> GetItemsList(string id)
        {
            var cart = LoadExistingCart(id);
            return cart.Items.Select(i => Mapping.Map<DAL.Item, BAL.Item>(i)).ToList();
        }

        public void UpdateItem(BAL.Item changedItem)
        {
            ValidateItem(changedItem);
            var newDbItem = Mapping.Map<BAL.Item, DAL.Item>(changedItem);
            cartRepository.BulkCartUpdate(cart =>
            {
                var oldItemPlace = cart.Items.FindIndex(oldItem => newDbItem.Id == oldItem.Id);
                if (oldItemPlace != -1)
                {
                    cart.Items.RemoveAt(oldItemPlace);
                    cart.Items.Insert(oldItemPlace, newDbItem);
                }
            });
        }

        public void RemoveItem(string cartId, int itemId)
        {
            var cart = LoadExistingCart(cartId);
            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                cart.Items.Remove(item);
                cartRepository.SaveCart(cart);
            }
        }

        private Cart LoadExistingCart(string id)
        {
            var cart = cartRepository.LoadCart(id);
            if (cart is not null)
                return cart;
            else
                throw new CartNotFoundException();
        }

        private static void ValidateItem(BAL.Item item)
        {
            var errors = new List<string>();
            if (item.Id <= 0)
                errors.Add("invalid Id");
            if (string.IsNullOrEmpty(item.Name))
                errors.Add("invalid name");
            if (item.Price < 0)
                errors.Add("negative price");
            if (item.Quantity <= 0)
                errors.Add("quantity should be positive");
            if (errors.Any())
                throw new InvalidItemException("Invalid item: " + String.Join(", ", errors));
        }
    }
}
