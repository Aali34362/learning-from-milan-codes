using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;
using MediatR;

namespace Application.Products.UpdateProduct;

internal class UpdateProductCommandHandler
    : ICommandHandler<UpdateProductCommand>
{
    private readonly IDocumentSession _session;
    private readonly IPublisher _publisher;

    public UpdateProductCommandHandler(IDocumentSession session, IPublisher publisher)
    {
        _session = session;
        _publisher = publisher;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(new Error(
                "Product.NotFound",
                $"The product with the Id = '{request.Id}' was not found."));
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Tags = request.Tags;

        _session.Update(product);

        await _session.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(
            new ProductUpdatedEvent
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            },
            cancellationToken);

        return Result.Success();
    }
}
