using Domain.Primitives;

namespace Domain.Customers;

public class Customer : Entity<CustomerId>
{
    public Customer(CustomerId id, string email, string name)
        : base(id)
    {
        Email = email;
        Name = name;
    }

    public string Email { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;
}