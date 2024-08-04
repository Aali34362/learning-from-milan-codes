namespace Modules.Notifications.Infrastructure.Email.Configuration;

/// <summary>
/// Represents the Mailersend options.
/// </summary>
internal sealed class MailersendOptions
{
    /// <summary>
    /// Gets the base URL.
    /// </summary>
    public string BaseUrl { get; init; } = string.Empty;

    /// <summary>
    /// Gets the access token.
    /// </summary>
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// Gets the sender email.
    /// </summary>
    public string SenderEmail { get; init; } = string.Empty;

    /// <summary>
    /// Gets the templates.
    /// </summary>
    public MailersendTemplates Templates { get; init; } = new();
}
