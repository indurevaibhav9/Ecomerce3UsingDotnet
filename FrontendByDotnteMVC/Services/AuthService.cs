using FrontendByDotnteMVC.Models.Customer;
using System.Text;
using System.Text.Json;
using UserService.Models;

namespace FrontendByDotnteMVC.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            string url = $"login?userName={Uri.EscapeDataString(username)}&Password={Uri.EscapeDataString(password)}";

            // Call GET API
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            // Return token from backend
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> RegisterAsync(RegisterDto model)
        {
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsJsonAsync(
             "customer/CreateCustomer",
             model
            );

            return response.IsSuccessStatusCode;
        }

    }
}
