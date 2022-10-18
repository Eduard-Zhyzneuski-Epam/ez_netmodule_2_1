using LiteDB;

namespace NetModule2_1.DAL
{
    public class Cart
    {
        public string Id { get; set; }
        public List<Item> Items { get; set; }
    }
}
