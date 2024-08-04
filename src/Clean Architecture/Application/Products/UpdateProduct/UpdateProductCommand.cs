using Application.Abstractions.Messaging;

namespace Application.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    long Id,
    string Name,
    decimal Price,
    List<string> Tags) : ICommand;
