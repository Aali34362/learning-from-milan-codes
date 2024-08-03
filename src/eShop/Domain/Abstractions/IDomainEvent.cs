using MediatR;

namespace Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid Id { get; init; }

    DateTime OccurredOnUtc { get; init; }
}
