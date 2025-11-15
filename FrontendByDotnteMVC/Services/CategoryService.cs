using FrontendByDotnteMVC.Models;
using ProductService.Models;
using System.Net.Http.Json;

namespace FrontendByDotnteMVC.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        // Get all categories
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>("category/GetAllCateogory");
        }

        // Get category by ID
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Category>($"category/{id}");
        }

        // Create category
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            var body = new { categoryName = category.CategoryName };

            var response = await _httpClient.PostAsJsonAsync("category/CreateCategory", body);
            return response.IsSuccessStatusCode;
        }

        // Update category
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var body = new
            {
                id = category.Id,
                categoryName = category.CategoryName
            };

            var response = await _httpClient.PutAsJsonAsync("category/UpdateCategory", body);
            return response.IsSuccessStatusCode;
        }

        // Delete category
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"category/DeleteCategory/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
