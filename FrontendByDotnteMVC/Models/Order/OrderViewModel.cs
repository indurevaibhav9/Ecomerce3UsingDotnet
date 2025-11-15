using System;

namespace FrontendByDotnetMVC.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }
}
