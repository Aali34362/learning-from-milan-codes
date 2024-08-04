using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Products.GetProducts;

internal sealed class GetProductsQueryHandler
    : IQueryHandler<GetProductsQuery, List<ProductResponse>>
{
    private readonly IQuerySession _session;
    private readonly ICacheService _cacheService;

    public GetProductsQueryHandler(IQuerySession session, ICacheService cacheService)
    {
        _session = session;
        _cacheService = cacheService;
    }

    public async Task<Result<List<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
        await _cacheService.GetAsync(
            "products",
            async () =>
            {
                IReadOnlyList<ProductResponse> products = await _session
                    .Query<Product>()
                    .Select(p => new ProductResponse(
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Tags))
                    .ToListAsync(cancellationToken);

                return products.ToList();
            },
            cancellationToken);
}
