using ProductService.Models;

namespace ProductService.Interfaces
{
    public interface IProductRepository
    {
        public Task<Models.Product> CreateProduct(CreateProductDTO product);
        public Task<Models.Product> UpdateProduct(Models.Product product);
        public Task<bool> DeleteProduct(int productId);
        public Task<Models.Product> GetProduct(int productId);
        public Task<List<Models.Product>> GetAllProducts();
    }
}
