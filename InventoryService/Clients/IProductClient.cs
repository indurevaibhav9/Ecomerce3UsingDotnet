namespace InventoryService.Clients
{
    public interface IProductClient
    {
        Task<bool> IsProductValidAsync(int productId);
    }
}
