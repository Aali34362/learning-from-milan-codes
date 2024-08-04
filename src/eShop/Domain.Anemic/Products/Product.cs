namespace Domain.Anemic.Products;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;
}
