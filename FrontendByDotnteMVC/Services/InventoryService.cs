using FrontendByDotnetMVC.Models.Inventory;
using System.Net.Http.Json;

namespace FrontendByDotnteMVC.Services
{
    public class InventoryService
    {
        private readonly HttpClient _httpClient;

        public InventoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<List<InventoryViewModel>> GetInventoryAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<InventoryViewModel>>("/inventory");
        }

        public async Task<InventoryViewModel?> GetInventoryByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<InventoryViewModel>($"/inventory/{id}");
        }

        public async Task<bool> CreateInventoryAsync(CreateInventoryViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("/inventory", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateInventoryAsync(int id, InventoryViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"/inventory/{id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/inventory/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
