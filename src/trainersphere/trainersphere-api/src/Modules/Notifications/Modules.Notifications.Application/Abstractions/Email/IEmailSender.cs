namespace Modules.Notifications.Application.Abstractions.Email;

/// <summary>
/// Represents the email sender interface.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends the welcome email using the specified request.
    /// </summary>
    /// <param name="welcomeEmailRequest">The welcome email request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The completed task.</returns>
    Task SendWelcomeAsync(WelcomeEmailRequest welcomeEmailRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends the client invitation email using the specified request.
    /// </summary>
    /// <param name="clientInvitationEmailRequest">The client invitation email request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The completed task.</returns>
    Task SendClientInvitationAsync(ClientInvitationEmailRequest clientInvitationEmailRequest, CancellationToken cancellationToken = default);
}
