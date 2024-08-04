using MediatR;

namespace Application.Products.UpdateProduct;

public record ProductUpdatedEvent : INotification
{
    public long Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public decimal Price { get; init; }
}
