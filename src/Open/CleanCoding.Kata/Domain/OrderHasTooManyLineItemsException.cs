namespace Domain;

public sealed class OrderHasTooManyLineItemsException : Exception
{
    public OrderHasTooManyLineItemsException(long orderId)
        : base($"The order {orderId} has too many items")
    {
        OrderId = orderId;
    }

    public long OrderId { get; }
}