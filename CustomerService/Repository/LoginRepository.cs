using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Repository
{
    public class LoginRepository : ILogin
    {
        private readonly UserDbContext _context;
        public LoginRepository(UserDbContext context)
        {
            _context = context;
        }
        public Task<Customer> Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return Task.FromResult<Customer>(null);
            }

            var customer = _context.Customers
               .FirstOrDefault(c => c.UserName == userName && c.Password == password);
            return Task.FromResult(customer);
        }

        public Task<bool> Logout(int customerId)
        {
            var customer = _context.Customers
               .FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
                {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
            // Perform logout operations if needed (e.g., update last logout time)
        }
    }
}
