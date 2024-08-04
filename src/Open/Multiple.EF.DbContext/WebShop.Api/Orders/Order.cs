namespace WebShop.Api.Orders;

public class Order
{
    public Guid Id { get; set; }

    public decimal TotalPrice { get; set; }

    public List<LineItem> LineItems { get; set; } = new();
}