using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand>
{
    private readonly IDocumentSession _session;

    public CreateProductCommandHandler(IDocumentSession session)
    {
        _session = session;
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

        return Result.Success();
    }
}
