namespace OrderService.Clients
{
    public interface IInventoryClient
    {
        Task<bool> HasSufficientStockAsync(int productId, int requiredQty);
    }
}
