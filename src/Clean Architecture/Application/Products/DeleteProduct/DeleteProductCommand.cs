using Application.Abstractions.Messaging;

namespace Application.Products.DeleteProduct;

public sealed record DeleteProductCommand(long Id) : ICommand;
