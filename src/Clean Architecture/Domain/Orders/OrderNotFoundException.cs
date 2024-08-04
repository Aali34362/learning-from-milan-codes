namespace Domain.Orders;

public sealed class OrderNotFoundException : Exception
{
    public OrderNotFoundException(OrderId id)
        : base($"The order with the ID = {id.Value} was not found")
    {
    }
}
