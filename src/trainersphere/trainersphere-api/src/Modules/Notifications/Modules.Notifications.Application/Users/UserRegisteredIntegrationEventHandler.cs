using Application.EventBus;
using Modules.Notifications.Application.Abstractions.Email;
using Modules.Users.IntegrationEvents;

namespace Modules.Notifications.Application.Users;

/// <summary>
/// Represents the <see cref="UserRegisteredIntegrationEvent"/> handler.
/// </summary>
internal sealed class UserRegisteredIntegrationEventHandler : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    private readonly IEmailSender _emailSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegisteredIntegrationEventHandler"/> class.
    /// </summary>
    /// <param name="emailSender">The email sender.</param>
    public UserRegisteredIntegrationEventHandler(IEmailSender emailSender) => _emailSender = emailSender;

    /// <inheritdoc />
    public override async Task Handle(UserRegisteredIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await _emailSender.SendWelcomeAsync(
            new WelcomeEmailRequest(
                integrationEvent.Email,
                $"{integrationEvent.FirstName} {integrationEvent.LastName}"),
            cancellationToken);
}
