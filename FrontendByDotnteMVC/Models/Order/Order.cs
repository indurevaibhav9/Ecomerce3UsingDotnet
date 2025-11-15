namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get  
            {
                return OrderItems != null ? OrderItems.Sum(item => item.Price * item.Quantity) : 0;
            } 
        } 
        public DateTime OrderDate { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
