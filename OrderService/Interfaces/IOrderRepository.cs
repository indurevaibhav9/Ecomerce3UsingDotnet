namespace OrderService.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Models.Order>> GetAllOrdersAsync();
        Task<Models.Order?> GetOrderByIdAsync(int id);
        Task<Models.Order> CreateOrderAsync(Models.Order order);
        Task<Models.Order?> UpdateOrderAsync(int id, Models.Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
