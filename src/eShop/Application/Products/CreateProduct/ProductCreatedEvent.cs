namespace Application.Products.CreateProduct;

public record ProductCreatedEvent
{
    public long Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public decimal Price { get; init; }
}
