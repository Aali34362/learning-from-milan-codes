namespace Modules.Notifications.Application.Abstractions.Email;

/// <summary>
/// Represents the welcome email request.
/// </summary>
/// <param name="Email">The email.</param>
/// <param name="Name">The name.</param>
public sealed record WelcomeEmailRequest(string Email, string Name);
