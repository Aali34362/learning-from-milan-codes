using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Products.UpdateProduct;

internal class UpdateProductCommandHandler
    : ICommandHandler<UpdateProductCommand>
{
    private readonly IDocumentSession _session;

    public UpdateProductCommandHandler(IDocumentSession session)
    {
        _session = session;
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

        return Result.Success();
    }
}
