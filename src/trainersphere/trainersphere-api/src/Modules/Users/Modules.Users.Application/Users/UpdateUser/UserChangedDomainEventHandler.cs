using Application.EventBus;
using Application.Messaging;
using Modules.Users.Domain.Users.Events;
using Modules.Users.IntegrationEvents;

namespace Modules.Users.Application.Users.UpdateUser;

/// <summary>
/// Represents the <see cref="UserChangedDomainEvent"/> class.
/// </summary>
internal sealed class UserChangedDomainEventHandler : IDomainEventHandler<UserChangedDomainEvent>
{
    private readonly IEventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserChangedDomainEventHandler"/> class.
    /// </summary>
    /// <param name="eventBus">The event bus.</param>
    public UserChangedDomainEventHandler(IEventBus eventBus) => _eventBus = eventBus;

    public async Task Handle(UserChangedDomainEvent notification, CancellationToken cancellationToken) =>
        await _eventBus.PublishAsync(
            new UserChangedIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.UserId.Value,
                notification.FirstName,
                notification.LastName,
                notification.Roles),
            cancellationToken);
}
