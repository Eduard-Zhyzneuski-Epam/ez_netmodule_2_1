using LiteDB;
using NetModule2_1.DAL;

namespace NetModule2_1
{
    internal static class DataSetup
    {
        internal static void Setup()
        {
            File.Delete("carts.db");
            using var db = new LiteDatabase("carts.db");
            var carts = db.GetCollection<Cart>();
            carts.Insert(new[]
            {
                new Cart
                {
                    Id = "First",
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Id = 1,
                            Name = "Teacup",
                            Image = null,
                            Price = 5,
                            Quantity = 1
                        },
                        new Item
                        {
                            Id = 2,
                            Name = "Satellite dish",
                            Image = new Image
                            {
                                Url = "http://example.com/satellite.jpg",
                                Alt = "Satellite dish"
                            },
                            Price = 500,
                            Quantity = 1
                        }
                    }
                },
                new Cart
                {
                    Id = "Second",
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Id = 2,
                            Name = "Satellite dish",
                            Image = new Image
                            {
                                Url = "http://example.com/satellite.jpg",
                                Alt = "Satellite dish"
                            },
                            Price = 500,
                            Quantity = 2
                        }
                    }
                }
            });
        }
    }
}
