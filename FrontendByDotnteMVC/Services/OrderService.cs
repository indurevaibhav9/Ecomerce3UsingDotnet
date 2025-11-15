using FrontendByDotnetMVC.Models.Order;
using OrderService.Models;
using System.Net.Http.Json;

namespace FrontendByDotnteMVC.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public async Task<List<OrderViewModel>> GetOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<OrderViewModel>>("order");
        }

        public async Task<OrderViewModel?> GetOrderByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<OrderViewModel>($"order/{id}");
        }

        public async Task<bool> CreateOrderAsync(CreateOrderDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("order", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"order/{id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"order/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
