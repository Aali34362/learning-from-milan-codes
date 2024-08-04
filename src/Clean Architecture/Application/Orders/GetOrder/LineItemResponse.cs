namespace Application.Orders.GetOrder;

public class LineItemResponse
{
    public Guid LineItemId { get; init; }
    public decimal Price { get; init; }
}