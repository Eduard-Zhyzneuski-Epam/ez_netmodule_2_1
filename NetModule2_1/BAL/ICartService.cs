namespace NetModule2_1.BAL
{
    public interface ICartService
    {
        List<Item> GetItemsList(string id);
        void RemoveItem(string cartId, int itemId);
        void AddItem(string id, Item item);
        void UpdateItem(Item item);
    }
}
