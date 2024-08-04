using Application.Abstractions.EventBus;
using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;
using MediatR;

namespace Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand>
{
    private readonly IDocumentSession _session;
    private readonly IEventBus _eventBus;
    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(IDocumentSession session, IEventBus eventBus, IPublisher publisher)
    {
        _session = session;
        _eventBus = eventBus;
        _publisher = publisher;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Tags = request.Tags
        };

        _session.Store(product);

        await _session.SaveChangesAsync(cancellationToken);

        var productCreatedEvent = new ProductCreatedEvent
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };

        await _publisher.Publish(productCreatedEvent, cancellationToken);

        await _eventBus.PublishAsync(
            productCreatedEvent,
            cancellationToken);

        return Result.Success();
    }
}
