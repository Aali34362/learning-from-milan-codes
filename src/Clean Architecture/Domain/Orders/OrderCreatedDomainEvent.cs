using Domain.Primitives;

namespace Domain.Orders;

public record OrderCreatedDomainEvent(Guid Id, OrderId OrderId) : DomainEvent(Id);
