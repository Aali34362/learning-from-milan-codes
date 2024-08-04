namespace Domain.Orders;

public record OrderSummary(
    Guid Id,
    Guid CustomerId,
    string CustomerName,
    decimal TotalPrice,
    List<OrderSummary.LineItem> LineItems)
{
    public record LineItem(
        Guid Id,
        Guid ProductId,
        string ProductName,
        string ProductSku,
        decimal PriceAmount,
        string PriceCurrency);
}
