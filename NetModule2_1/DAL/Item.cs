namespace NetModule2_1.DAL
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Image
    {
        public string Url { get; set; }
        public string Alt { get; set; }
    }
}
