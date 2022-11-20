using LiteDB;
using NetModule2_1.DAL;

namespace NetModule2_1.LiteDb
{
    internal class CartRepository : ICartRepository
    {
        private readonly string connectionString = "carts.db";

        public CartRepository() 
        {
            BsonMapper.Global.Entity<Cart>().Id(c => c.Id, false);
        }

        public Cart LoadCart(string id)
        {
            using var db = new LiteDatabase(connectionString);
            var carts = db.GetCollection<Cart>();
            return carts.Find(c => c.Id == id, 0, 1).FirstOrDefault();
        }

        public void SaveCart(Cart cart)
        {
            using var db = new LiteDatabase(connectionString);
            var carts = db.GetCollection<Cart>();
            carts.Upsert(cart);
        }
    }
}
