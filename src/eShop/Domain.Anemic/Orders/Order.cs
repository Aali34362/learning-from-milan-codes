namespace Domain.Anemic.Orders;

public class Order
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public List<LineItem> LineItems { get; set; } = new();

    public OrderStatus Status { get; set; }
}