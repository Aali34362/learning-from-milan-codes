using System.Linq.Expressions;
using Application.Abstractions;
using Application.Data;
using Application.Products.GetById;
using Domain.Products;
using MediatR;

namespace Application.Products.Get;

internal sealed class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, PagedList<ProductResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILinkService _linkService;

    public GetProductsQueryHandler(IApplicationDbContext context, ILinkService linkService)
    {
        _context = context;
        _linkService = linkService;
    }

    public async Task<PagedList<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Product> productsQuery = _context.Products;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            productsQuery = productsQuery.Where(p =>
                p.Name.Contains(request.SearchTerm) ||
                ((string)p.Sku).Contains(request.SearchTerm));
        }

        if (request.SortOrder?.ToLower() == "desc")
        {
            productsQuery = productsQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            productsQuery = productsQuery.OrderBy(GetSortProperty(request));
        }

        var productResponsesQuery = productsQuery
            .Select(p => new ProductResponse(
                p.Id.Value,
                p.Name,
                p.Sku.Value,
                p.Price.Currency,
                p.Price.Amount));

        var products = await PagedList<ProductResponse>.CreateAsync(
            productResponsesQuery,
            request.Page,
            request.PageSize);

        AddLinksForPagedProducts(request, products);

        return products;
    }

    private static Expression<Func<Product, object>> GetSortProperty(GetProductsQuery request) =>
        request.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "sku" => product => product.Sku,
            "amount" => product => product.Price.Amount,
            "currency" => product => product.Price.Currency,
            _ => product => product.Id
        };

    private void AddLinksForPagedProducts(GetProductsQuery query, PagedList<ProductResponse> products)
    {
        products.Links.Add(
            _linkService.Generate(
                "GetProducts",
                new
                {
                    searchTerm = query.SearchTerm,
                    sortColumn = query.SortColumn,
                    sortOrder = query.SortOrder,
                    page = query.Page,
                    pageSize = query.PageSize
                },
                "self",
                "GET"));

        if (products.HasNextPage)
        {
            products.Links.Add(
                _linkService.Generate(
                    "GetProducts",
                    new
                    {
                        searchTerm = query.SearchTerm,
                        sortColumn = query.SortColumn,
                        sortOrder = query.SortOrder,
                        page = query.Page + 1,
                        pageSize = query.PageSize
                    },
                    "next-page",
                    "GET"));
        }

        if (products.HasPreviousPage)
        {
            products.Links.Add(
                _linkService.Generate(
                    "GetProducts",
                    new
                    {
                        searchTerm = query.SearchTerm,
                        sortColumn = query.SortColumn,
                        sortOrder = query.SortOrder,
                        page = query.Page - 1,
                        pageSize = query.PageSize
                    },
                    "previous-page",
                    "GET"));
        }
    }
}
