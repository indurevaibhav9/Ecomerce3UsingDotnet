using OrderService.Models;

namespace OrderService.Client
{
    public interface IProductClient
    {
        Task<bool> IsProductValidAsync(int productId);
        public Task<Product?> GetProductByIdAsync(int productId);

    }
}
