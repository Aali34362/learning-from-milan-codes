using Application.EventBus;
using Application.Messaging;
using Modules.Users.Domain.Users.Events;
using Modules.Users.IntegrationEvents;

namespace Modules.Users.Application.Users.RegisterUser;

/// <summary>
/// Represents the <see cref="UserRegisteredDomainEvent"/> handler.
/// </summary>
internal sealed class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
{
    private readonly IEventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegisteredDomainEventHandler"/> class.
    /// </summary>
    /// <param name="eventBus">The event bus.</param>
    public UserRegisteredDomainEventHandler(IEventBus eventBus) => _eventBus = eventBus;

    /// <inheritdoc />
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken) =>
        await _eventBus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.UserId.Value,
                notification.UserRegistrationId?.Value,
                notification.Email,
                notification.FirstName,
                notification.LastName,
                notification.Roles),
            cancellationToken);
}
