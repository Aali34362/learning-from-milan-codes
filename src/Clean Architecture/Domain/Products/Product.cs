namespace Domain.Products;

public class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public List<string> Tags { get; set; } = new();
}
