using Domain.Rich.Customers;
using Domain.Rich.Products;

namespace Domain.Rich.Orders;

public class Order
{
    private readonly HashSet<LineItem> _lineItems = new();

    private Order()
    {
    }

    public OrderId Id { get; private set; }

    public CustomerId CustomerId { get; private set; }

    public OrderStatus Status { get; private set; }

    public static Order Create(CustomerId customerId)
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId,
            Status = OrderStatus.Pending
        };

        return order;
    }

    public void AddLineItem(ProductId productId, Money price)
    {
        var lineItem = new LineItem(new LineItemId(Guid.NewGuid()), Id, productId, price);

        _lineItems.Add(lineItem);
    }
}