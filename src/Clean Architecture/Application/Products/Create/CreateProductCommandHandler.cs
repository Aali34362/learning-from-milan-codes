using Application.Data;
using Domain.Products;
using MediatR;

namespace Application.Products.Create;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            new ProductId(Guid.NewGuid()),
            request.Name,
            new Money(request.Currency, request.Amount),
            Sku.Create(request.Sku)!);

        _productRepository.Add(product);

        return Task.CompletedTask;
    }
}
