using MediatR;

namespace Application.Products.Create;

public record CreateProductCommand(
    string Name,
    string Sku,
    string Currency,
    decimal Amount) : IRequest;

public record CreateProductRequest(
    string Name,
    string Sku,
    string Currency,
    decimal Amount);