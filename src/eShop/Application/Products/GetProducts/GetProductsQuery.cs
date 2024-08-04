using Application.Abstractions.Messaging;

namespace Application.Products.GetProducts;

public sealed record GetProductsCursorQuery(long Cursor, int PageSize) : IQuery<CursorResponse<List<ProductResponse>>>;

public sealed record GetProductsQuery(int Page, int PageSize) : IQuery<List<ProductResponse>>;
