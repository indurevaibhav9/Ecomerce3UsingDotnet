namespace OrderService.Models
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        public ICollection<OrderItemDtos> OrderItemDtos { get; set; }
    }

    public class OrderItemDtos
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}


