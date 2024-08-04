namespace Modules.Notifications.Infrastructure.Email.Contracts;

/// <summary>
/// Represents the recipient.
/// </summary>
/// <param name="Email">The email.</param>
internal sealed record Recipient(string Email);
