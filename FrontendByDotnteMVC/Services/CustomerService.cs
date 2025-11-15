using FrontendByDotnteMVC.Models;
using FrontendByDotnteMVC.Models.Customer;
using System.Net.Http.Json;

namespace FrontendByDotnteMVC.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        // -------------------- GET ALL CUSTOMERS --------------------
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Customer>>(
                "customer/GetAllCustomer"
            );
        }

        // -------------------- GET CUSTOMER BY ID --------------------
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Customer>(
                $"customer/{id}"
            );
        }

        // -------------------- CREATE CUSTOMER --------------------
        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "customer/CreateCustomer",
                customer
            );

            return response.IsSuccessStatusCode;
        }

        // -------------------- UPDATE CUSTOMER --------------------
        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PutAsJsonAsync(
                "customer/UpdateCustomer",
                customer
            );

            return response.IsSuccessStatusCode;
        }

        // -------------------- DELETE CUSTOMER --------------------
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(
                $"customer/DeleteCustomer/{id}"
            );

            return response.IsSuccessStatusCode;
        }

        // -------------------- LOGIN --------------------
        public async Task<string> LoginAsync(string userName, string password)
        {
            var response = await _httpClient.GetAsync(
                $"login?userName={userName}&Password={password}"
            );

            return await response.Content.ReadAsStringAsync();
        }

        // -------------------- LOGOUT --------------------
        public async Task<bool> LogoutAsync(int id)
        {
            var response = await _httpClient.PostAsync(
                $"logout/{id}",
                null
            );

            return response.IsSuccessStatusCode;
        }
    }
}
