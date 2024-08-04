using Application.Abstractions.Caching;
using Application.Products.CreateProduct;
using Application.Products.DeleteProduct;
using Application.Products.UpdateProduct;
using MediatR;

namespace Application.Products;

internal class CacheInvalidationProductHandler :
    INotificationHandler<ProductCreatedEvent>,
    INotificationHandler<ProductUpdatedEvent>,
    INotificationHandler<ProductDeletedEvent>
{
    private readonly ICacheService _cacheService;

    public CacheInvalidationProductHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    public Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    private async Task HandleInternal(long productId, CancellationToken cancellationToken)
    {
        await _cacheService.RemoveAsync("products", cancellationToken);
    }
}
