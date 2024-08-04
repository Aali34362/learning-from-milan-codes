using Application.Abstractions.Messaging;

namespace Application.Products.GetProducts;

public sealed record GetProductsQuery : IQuery<List<ProductResponse>>;
