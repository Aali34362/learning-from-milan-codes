using Domain.Orders;
using IntegrationEvents;
using MediatR;

namespace Application.Orders.Create;

internal sealed class OrderCreatedDomainEventHandler
    : INotificationHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        new OrderCreatedIntegrationEvent(notification.OrderId.Value);

        return Task.CompletedTask;
    }
}
