using FrontendByDotnteMVC.Models;
using OrderService.Models;
using ProductService.Models;

namespace FrontendByDotnteMVC.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        // -------------------- GET ALL PRODUCTS --------------------
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("product");
        }

        // -------------------- GET PRODUCT BY ID --------------------
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"product/{id}");
        }

        // -------------------- CREATE PRODUCT --------------------
        public async Task<bool> CreateProductAsync(CreateProductDTO product)
        {
            var response = await _httpClient.PostAsJsonAsync("product", product);
            return response.IsSuccessStatusCode;
        }

        // -------------------- UPDATE PRODUCT --------------------
        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"product/{id}", product);
            return response.IsSuccessStatusCode;
        }

        // -------------------- DELETE PRODUCT --------------------
        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"product/{id}");
            return response.IsSuccessStatusCode;
        }

        // =================================================================
        //                           CATEGORY METHODS
        // =================================================================

        // -------------------- GET ALL CATEGORIES --------------------
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>(
                "category/GetAllCateogory"
            );
        }

        // -------------------- GET CATEGORY BY ID --------------------
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Category>($"category/{id}");
        }

        // -------------------- CREATE CATEGORY --------------------
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync("category/CreateCategory", category);
            return response.IsSuccessStatusCode;
        }

        // -------------------- UPDATE CATEGORY --------------------
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var response = await _httpClient.PutAsJsonAsync("category/UpdateCategory", category);
            return response.IsSuccessStatusCode;
        }

        // -------------------- DELETE CATEGORY --------------------
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"category/DeleteCategory/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
