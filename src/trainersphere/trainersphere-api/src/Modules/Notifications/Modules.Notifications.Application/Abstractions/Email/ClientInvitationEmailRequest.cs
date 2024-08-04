namespace Modules.Notifications.Application.Abstractions.Email;

/// <summary>
/// Represents the client invitation email request.
/// </summary>
/// <param name="Email">The email.</param>
/// <param name="Trainer">The trainer.</param>
public sealed record ClientInvitationEmailRequest(string Email, Guid InvitationId, string Trainer);
