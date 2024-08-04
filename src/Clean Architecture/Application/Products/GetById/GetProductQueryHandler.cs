using Application.Abstractions.Links;
using Application.Data;
using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.GetById;

internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ILinkService _linkService;

    public GetProductQueryHandler(IApplicationDbContext context, ILinkService linkService)
    {
        _context = context;
        _linkService = linkService;
    }

    public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context
            .Products
            .Where(p => p.Id == request.ProductId)
            .Select(p => new ProductResponse(
                p.Id.Value,
                p.Name,
                p.Sku.Value,
                p.Price.Currency,
                p.Price.Amount))
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        AddLinksForProduct(product);

        return product;
    }

    private void AddLinksForProduct(ProductResponse productResponse)
    {
        productResponse.Links.Add(
            _linkService.Generate(
                "GetProduct",
                new { id = productResponse.Id },
                "self",
                "GET"));

        productResponse.Links.Add(
            _linkService.Generate(
                "UpdateProduct",
                new { id = productResponse.Id },
                "update-product",
                "PUT"));

        productResponse.Links.Add(
            _linkService.Generate(
                "DeleteProduct",
                new { id = productResponse.Id },
                "delete-product",
                "DELETE"));
    }
}
