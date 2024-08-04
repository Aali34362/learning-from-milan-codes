using Application.Products.GetById;
using MediatR;

namespace Application.Products.Get;

public record GetProductsQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<PagedList<ProductResponse>>;
