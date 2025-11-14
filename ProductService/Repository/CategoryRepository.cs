
using ProductService.Data;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Models.DTOs;

namespace ProductService.Repository
{
    public class CategoryRepository  : ICategory
    {
        private readonly ProductDbContext _context;
        public CategoryRepository(ProductDbContext context)
        {
            _context = context;
        }
    

        public async Task<Category> CreateCategory(CreateCategory category)
        {
            Category category2 = new Category
            {
                CategoryName = category.CategoryName
            };

            _context.Categories.Add(category2);
            await _context.SaveChangesAsync();
            return category2;
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
             Category category = await GetCategory(categoryId);
            if (category != null)
                {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public Task<List<Category>> GetAllCategories()
        {
            var result =   _context.Categories.ToList();
            return Task.FromResult(result);
        }

        public async Task<Category> GetCategory(int categoryId)
        {
           Category category = await _context.Categories.FindAsync(categoryId);
              return category;
        }

        public Task<Category> UpdateCategory(Category category)
        {
            Category existingCategory =  _context.Categories.Find(category.Id);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                _context.Categories.Update(existingCategory);
                _context.SaveChanges();
                return Task.FromResult(existingCategory);
            }
            return Task.FromResult<Category>(null);
        }
    }
}
