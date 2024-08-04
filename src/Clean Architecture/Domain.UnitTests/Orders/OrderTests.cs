using Bogus;
using Domain.Customers;
using Domain.Orders;

namespace Domain.UnitTests.Orders;

public class OrderTests
{
    [Theory]
    [ClassData(typeof(OrderCreateTestData))]
    public void Create_Should_RaiseDomainEvent(CustomerId customerId)
    {
        // Act
        var order = Order.Create(customerId);

        // Assert
        Assert.NotEmpty(order.GetDomainEvents().OfType<OrderCreatedDomainEvent>());
    }
}

public class OrderCreateTestData : TheoryData<CustomerId>
{
    public OrderCreateTestData()
    {
        var faker = new Faker();

        Add(new CustomerId(faker.Random.Guid()));
    }
}
