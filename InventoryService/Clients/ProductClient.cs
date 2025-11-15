using System.Net.Http;
using System.Threading.Tasks;

namespace InventoryService.Clients
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductClient> _logger;

        public ProductClient(HttpClient httpClient, ILogger<ProductClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> IsProductValidAsync(int productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"product/{productId}");
                return response.IsSuccessStatusCode; // 200 OK means valid
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling ProductService");
                return false;
            }
        }
    }
}
