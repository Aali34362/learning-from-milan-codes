namespace Modules.Notifications.Infrastructure.Email.Configuration;

/// <summary>
/// Represents the mailersend templates.
/// </summary>
internal sealed class MailersendTemplates
{
    /// <summary>
    /// Gets the welcome email template identifier.
    /// </summary>
    public string WelcomeEmail { get; init; } = string.Empty;

    /// <summary>
    /// Gets the client invitation email template identifier.
    /// </summary>
    public string ClientInvitationEmail { get; init; } = string.Empty;
}
