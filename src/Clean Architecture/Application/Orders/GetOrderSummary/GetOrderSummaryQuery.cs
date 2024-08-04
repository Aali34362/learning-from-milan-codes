using Domain.Orders;
using MediatR;

namespace Application.Orders.GetOrderSummary;

public record GetOrderSummaryQuery(Guid OrderId) : IRequest<OrderSummary?>;
