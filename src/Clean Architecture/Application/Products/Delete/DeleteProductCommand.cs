using Domain.Products;
using MediatR;

namespace Application.Products.Delete;

public record DeleteProductCommand(ProductId ProductId) : IRequest;
