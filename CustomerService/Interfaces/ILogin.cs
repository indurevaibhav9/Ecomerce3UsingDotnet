using UserService.Models;

namespace UserService.Interfaces
{
    public interface ILogin
    {
        public Task<Customer> Login(string userName, string password);
        public Task<bool> Logout(int customerId);
    }
}
