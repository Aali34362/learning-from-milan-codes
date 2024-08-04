using Application.Abstractions.Data;
using Application.Abstractions.Links;
using Dapper;
using Domain.Products;
using MediatR;

namespace Application.Products.GetById;

internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
{
    private readonly ILinkService _linkService;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetProductQueryHandler(ILinkService linkService, ISqlConnectionFactory sqlConnectionFactory)
    {
        _linkService = linkService;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.Create();

        var productResponse = await connection.QueryFirstOrDefaultAsync<ProductResponse>(
            $"""
            SELECT
                id AS {nameof(ProductResponse.Id)},
                name AS {nameof(ProductResponse.Name)},
                sku AS {nameof(ProductResponse.Sku)},
                price_currency AS {nameof(ProductResponse.Currency)},
                price_amount AS {nameof(ProductResponse.Amount)}
            FROM products
            WHERE id = @ProductId
            """,
            new
            {
                ProductId = request.ProductId.Value,
            });

        if (productResponse is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        AddLinksForProduct(productResponse);

        return productResponse;
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
