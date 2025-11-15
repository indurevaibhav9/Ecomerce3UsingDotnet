using System.ComponentModel.DataAnnotations;

namespace ProductService.Models
{
    public class CreateProductDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }

    }
}
