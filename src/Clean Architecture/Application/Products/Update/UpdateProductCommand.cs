using Domain.Products;
using MediatR;

namespace Application.Products.Update;

public record UpdateProductCommand(
    ProductId ProductId,
    string Name,
    string Sku,
    string Currency,
    decimal Amount) : IRequest;

public record UpdateProductRequest(
    string Name,
    string Sku,
    string Currency,
    decimal Amount);
