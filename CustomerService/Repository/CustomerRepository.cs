using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Repository
{
    public class CustomerRepository : ICustomer
    {
        private readonly UserDbContext _context;
        public CustomerRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> CreateCustomer(RegisterDto registerDto)
        {
            Customer customer = new Customer
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Password = registerDto.Password,
                Phone = registerDto.Phone
            };

            _context.Customers.Add(customer);

           await  _context.SaveChangesAsync();
            return customer;
        }

        public Task<bool> DeleteCustomer(string customerId)
        {
            Customer customer = _context.Customers.FirstOrDefault(c => c.Id.ToString() == customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            Customer customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            Customer  existingCustomerTask = await _context.Customers
               .FirstOrDefaultAsync(c => c.Id == customer.Id);

            existingCustomerTask.Email = customer.Email;
            existingCustomerTask.Phone = customer.Phone;
            existingCustomerTask.UserName = customer.UserName;
            existingCustomerTask.Password = customer.Password;
            existingCustomerTask.Name = customer.Name;

            await _context.SaveChangesAsync();
                return existingCustomerTask;

        }
    }
}
