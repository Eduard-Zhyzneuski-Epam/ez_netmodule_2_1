using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetModule2_1.API.Models
{
    public class Item
    {
        [Required, Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public Image? Image { get; set; }
        [Required, Range(typeof(decimal), "0.01", "1000000000000")]
        public decimal Price { get; set; }
        [Required, Range(1, int.MaxValue), DefaultValue(1)]
        public int Quantity { get; set; }
    }

    public class Image
    {
        public string Url { get; set; }
        public string? Alt { get; set; }
    }
}
