using System.Net.Http.Json;

namespace OrderService.Clients
{
    public class InventoryClient : IInventoryClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<InventoryClient> _logger;

        public InventoryClient(HttpClient httpClient, ILogger<InventoryClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> HasSufficientStockAsync(int productId, int requiredQty)
        {
            try
            {
                // Step 1: Get the current inventory details
                var inventory = await _httpClient.GetFromJsonAsync<InventoryResponse>($"api/inventorys/{productId}");
                _logger.LogInformation(inventory.ToString());

                if (inventory == null)
                {
                    _logger.LogWarning("No inventory record found for ProductId {ProductId}", productId);
                    return false;
                }

                if (inventory.Quantity < requiredQty)
                {
                    _logger.LogWarning("Insufficient stock for ProductId {ProductId}. Available: {Available}, Required: {Required}",
                        productId, inventory.Quantity, requiredQty);
                    return false;
                }

                // Step 2: Deduct stock quantity
                inventory.Quantity -= requiredQty;

                // Step 3: Update the inventory in the InventoryService (PUT request)
                var updateResponse = await _httpClient.PutAsJsonAsync($"api/inventorys/{inventory.ProductId}", inventory);

                if (!updateResponse.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to update inventory for ProductId {ProductId}. Status: {StatusCode}",
                        productId, updateResponse.StatusCode);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking/updating inventory for ProductId {ProductId}", productId);
                return false;
            }
        }

       

        private class InventoryResponse
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
