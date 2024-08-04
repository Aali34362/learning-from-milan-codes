using Application.Abstractions.Idempotency;
using MediatR;

namespace Application.Products.Create;

public record CreateProductCommand(
    Guid RequestId,
    string Name,
    string Sku,
    string Currency,
    decimal Amount) : IdempotentCommand(RequestId);

public record CreateProductRequest(
    string Name,
    string Sku,
    string Currency,
    decimal Amount);