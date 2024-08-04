using Domain.Products;
using MediatR;

namespace Application.Products.Update;

internal sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        product.Update(
            request.Name,
            new Money(request.Currency, request.Amount),
            Sku.Create(request.Sku)!);

        _productRepository.Update(product);
    }
}
