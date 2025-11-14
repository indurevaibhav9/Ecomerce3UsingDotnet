using UserService.Models;

namespace UserService.Interfaces
{
    public interface ICustomer
    {
        public Task<Customer> CreateCustomer(RegisterDto registerDto);
        public Task<Customer> UpdateCustomer(Customer customer);
        public Task<bool> DeleteCustomer(string customerId);
        public Task<Customer> GetCustomer(int customerId);
        public Task<List<Customer>> GetAllCustomers();
    }
}
