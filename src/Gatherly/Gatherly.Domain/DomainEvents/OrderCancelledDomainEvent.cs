namespace Gatherly.Domain.DomainEvents;

public sealed record OrderCancelledDomainEvent(Guid Id, Guid GatheringId) : DomainEvent(Id);
