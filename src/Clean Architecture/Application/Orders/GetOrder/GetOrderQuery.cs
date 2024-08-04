using MediatR;

namespace Application.Orders.GetOrder;

public record GetOrderQuery(Guid OrderId) : IRequest<OrderResponse>;
