using Application.EventBus;
using Application.Messaging;
using Modules.Training.Domain.Invitations.Events;
using Modules.Training.IntegrationEvents;

namespace Modules.Training.Application.Invitations.CancelInvitation;

/// <summary>
/// Represents the <see cref="InvitationCancelledDomainEvent"/> handler.
/// </summary>
internal sealed class InvitationCancelledDomainEventHandler : IDomainEventHandler<InvitationCancelledDomainEvent>
{
    private readonly IEventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvitationCancelledDomainEventHandler"/> class.
    /// </summary>
    /// <param name="eventBus">The event bus.</param>
    public InvitationCancelledDomainEventHandler(IEventBus eventBus) => _eventBus = eventBus;

    /// <inheritdoc />
    public async Task Handle(InvitationCancelledDomainEvent notification, CancellationToken cancellationToken) =>
        await _eventBus.PublishAsync(
            new InvitationCancelledIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.InvitationId.Value),
            cancellationToken);
}
