
using OrderService.Models;

namespace OrderService.Client
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;
        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> IsProductValidAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"api/products/{productId}");
            return response.IsSuccessStatusCode; // 200 OK means valid
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _httpClient.GetFromJsonAsync<Product>($"api/products/{productId}");
                return product;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
