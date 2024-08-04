using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;
using MediatR;

namespace Application.Products.DeleteProduct;

internal sealed class DeleteProductCommandHandler
    : ICommandHandler<DeleteProductCommand>
{
    private readonly IDocumentSession _session;
    private readonly IPublisher _publisher;

    public DeleteProductCommandHandler(IDocumentSession session, IPublisher publisher)
    {
        _session = session;
        _publisher = publisher;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _session.Delete<Product>(request.Id);

        await _session.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new ProductDeletedEvent { Id = request.Id }, cancellationToken);

        return Result.Success();
    }
}
