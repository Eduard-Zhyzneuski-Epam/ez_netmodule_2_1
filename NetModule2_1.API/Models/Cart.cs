using System.ComponentModel.DataAnnotations;

namespace NetModule2_1.API.Models
{
    public class Cart
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<Item> Items { get; set; }
    }
}
