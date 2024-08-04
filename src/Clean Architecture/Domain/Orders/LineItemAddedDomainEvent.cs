using Domain.Primitives;

namespace Domain.Orders;

public sealed record LineItemAddedDomainEvent(
    Guid Id,
    OrderId OrderId,
    LineItemId LineItemId) : DomainEvent(Id);
