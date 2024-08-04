using Application.Abstractions.Messaging;

namespace Application.Products.Create;

public record CreateProductCommand(
    string Name,
    string Sku,
    string Currency,
    decimal Amount) : ICommand<Guid>;

public record CreateProductRequest(
    string Name,
    string Sku,
    string Currency,
    decimal Amount);