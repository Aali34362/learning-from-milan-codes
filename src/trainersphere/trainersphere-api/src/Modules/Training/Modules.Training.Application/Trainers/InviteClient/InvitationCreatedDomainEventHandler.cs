using Application.EventBus;
using Application.Messaging;
using Modules.Training.Domain.Invitations.Events;
using Modules.Training.IntegrationEvents;

namespace Modules.Training.Application.Trainers.InviteClient;

/// <summary>
/// Represents the <see cref="InvitationCreatedDomainEvent"/> handler.
/// </summary>
internal sealed class InvitationCreatedDomainEventHandler : IDomainEventHandler<InvitationCreatedDomainEvent>
{
    private readonly IEventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvitationCreatedDomainEventHandler"/> class.
    /// </summary>
    /// <param name="eventBus">The event bus.</param>
    public InvitationCreatedDomainEventHandler(IEventBus eventBus) => _eventBus = eventBus;

    /// <inheritdoc />
    public async Task Handle(InvitationCreatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _eventBus.PublishAsync(
            new InvitationSentIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.InvitationId.Value,
                notification.TrainerId.Value,
                notification.Email,
                notification.Sender.FirstName,
                notification.Sender.LastName),
            cancellationToken);
}
