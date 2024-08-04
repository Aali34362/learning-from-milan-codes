namespace Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(CustomerId id);

    Task<bool> IsEmailUniqueAsync(string email);

    void Add(Customer customer);
}
