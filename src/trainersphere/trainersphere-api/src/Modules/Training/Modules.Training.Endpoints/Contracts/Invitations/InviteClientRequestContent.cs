namespace Modules.Training.Endpoints.Contracts.Invitations;

/// <summary>
/// Represents the invite client request content.
/// </summary>
/// <param name="Email">The email.</param>
public sealed record InviteClientRequestContent(string Email);
