using Application.EventBus;
using Modules.Notifications.Application.Abstractions.Email;
using Modules.Training.IntegrationEvents;

namespace Modules.Notifications.Application.Invitations;

/// <summary>
/// Represents the <see cref="InvitationSentIntegrationEvent"/> handler.
/// </summary>
internal sealed class InvitationSentIntegrationEventHandler : IntegrationEventHandler<InvitationSentIntegrationEvent>
{
    private readonly IEmailSender _emailSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvitationSentIntegrationEventHandler"/> class.
    /// </summary>
    /// <param name="emailSender">The email sender.</param>
    public InvitationSentIntegrationEventHandler(IEmailSender emailSender) => _emailSender = emailSender;

    /// <inheritdoc />
    public override async Task Handle(InvitationSentIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await _emailSender.SendClientInvitationAsync(
            new ClientInvitationEmailRequest(
                integrationEvent.Email,
                integrationEvent.InvitationId,
                $"{integrationEvent.SenderFirstName} {integrationEvent.SenderLastName}"),
            cancellationToken);
}
