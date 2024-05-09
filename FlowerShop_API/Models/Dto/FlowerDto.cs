using System.ComponentModel.DataAnnotations;

namespace FlowerShop_API.Models.Dto
{
    public class FlowerDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
