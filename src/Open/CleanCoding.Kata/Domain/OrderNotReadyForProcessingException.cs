namespace Domain;

public sealed class OrderNotReadyForProcessingException : Exception
{
    public OrderNotReadyForProcessingException(long orderId)
        : base($"The order {orderId} isn't ready to process")
    {
        OrderId = orderId;
    }

    public long OrderId { get; }
}