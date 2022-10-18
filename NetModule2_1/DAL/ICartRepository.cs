namespace NetModule2_1.DAL
{
    internal interface ICartRepository
    {
        Cart LoadCart(string id);
        void SaveCart(Cart cart);
    }
}
