
using ProductService.Models;
using ProductService.Models.DTOs;

namespace ProductService.Interfaces
{
    public interface ICategory
    {
        public Task<Category> CreateCategory(CreateCategory category);
        public Task<Category> UpdateCategory(Category category);
        public Task<bool> DeleteCategory(int categoryId);
        public Task<Category> GetCategory(int categoryId);
        public Task<List<Category>> GetAllCategories();
    }
}
