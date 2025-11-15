using System.ComponentModel.DataAnnotations;

namespace FrontendByDotnetMVC.Models.Inventory
{
    public class CreateInventoryViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
