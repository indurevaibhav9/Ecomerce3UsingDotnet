namespace OrderService.Client
{
    public interface ICustomerClient
    {
        Task<bool> IsCustomerValidAsync(int customerId);
    }
}
