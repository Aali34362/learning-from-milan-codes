using Application.Abstractions.Messaging;

namespace Application.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    decimal Price,
    List<string> Tags) : ICommand;
