using Domain.Orders;
using Domain.Products;
using MediatR;

namespace Application.Orders.AddLineItem;

public record AddLineItemCommand(
    OrderId OrderId,
    ProductId ProductId,
    string Currency,
    decimal Amount) : IRequest;

public record AddLineItemRequest(Guid ProductId, string Currency, decimal Amount);
