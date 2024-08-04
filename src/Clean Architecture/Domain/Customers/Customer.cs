namespace Domain.Customers;

public class Customer
{
    public Customer(CustomerId id, string email, string name)
    {
        Id = id;
        Email = email;
        Name = name;
    }

    public CustomerId Id { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;
}