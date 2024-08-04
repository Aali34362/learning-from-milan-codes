using Domain.Primitives;

namespace Domain.Orders;

public record LineItemRemovedDomainEvent(
    Guid Id,
    OrderId OrderId,
    LineItemId LineItemId) : DomainEvent(Id);
