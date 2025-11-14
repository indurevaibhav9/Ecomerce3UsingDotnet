
namespace OrderService.Client
{
    public class CustomerClient : ICustomerClient
    {
        private readonly HttpClient _httpClient;
        public CustomerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsCustomerValidAsync(int customerId)
        {
            var response = await _httpClient.GetAsync($"api/Customers/{customerId}");

            if (response.IsSuccessStatusCode)
                return true; // Customer exists (200 OK)

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return false; // Customer not found (404)

            response.EnsureSuccessStatusCode(); // Throw if another error (500, 400, etc.)
            return false;
        }
    }
}
