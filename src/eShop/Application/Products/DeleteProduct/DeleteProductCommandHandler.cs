using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Products.DeleteProduct;

internal sealed class DeleteProductCommandHandler
    : ICommandHandler<DeleteProductCommand>
{
    private readonly IDocumentSession _session;

    public DeleteProductCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _session.Delete<Product>(request.Id);

        await _session.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
