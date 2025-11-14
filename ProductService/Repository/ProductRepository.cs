using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Interfaces;
using ProductService.Models;

namespace ProductService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly ProductDbContext _context;
        public ProductRepository(ILogger<ProductRepository> logger, ProductDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Product> CreateProduct(CreateProductDTO product)
        {
            Product product1 = new Product
            {
                ProductName = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ProductDescription = product.Description
            };

            _context.Products.Add(product1);
            await _context.SaveChangesAsync();
            return product1;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            Product product1 = await _context.Products.FindAsync(product.Id);
            if (product1 != null)
            {
                product1.ProductName = product.ProductName;
                product1.Price = product.Price;
                product1.ProductDescription = product.ProductDescription;
                product1.CategoryId = product.CategoryId;

                await _context.SaveChangesAsync();
            }
            return product1;
        }
    }
}
