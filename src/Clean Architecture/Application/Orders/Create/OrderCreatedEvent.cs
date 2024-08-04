using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

public record OrderCreatedEvent(OrderId OrderId) : INotification;
