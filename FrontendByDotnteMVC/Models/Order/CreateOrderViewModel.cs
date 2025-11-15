using System.ComponentModel.DataAnnotations;

namespace FrontendByDotnteMVC.Models.Order
{
    public class CreateOrderViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<CreateOrderItemViewModel> OrderItems { get; set; } = new();
    }

}
