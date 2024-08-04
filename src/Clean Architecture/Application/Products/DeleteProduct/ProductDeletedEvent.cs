using MediatR;

namespace Application.Products.DeleteProduct;

public record ProductDeletedEvent : INotification
{
    public long Id { get; init; }
}
