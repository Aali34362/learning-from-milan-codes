using MediatR;

namespace Application.Orders.Create;

public record CreateOrderCommand(Guid CustomerId) : IRequest;
