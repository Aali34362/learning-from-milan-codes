namespace Modules.Training.Application.Invitations.GetInvitationById;

/// <summary>
/// Represents the invitation response.
/// </summary>
public sealed record InvitationResponse
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Gets the sender first name.
    /// </summary>
    public string SenderFirstName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the sender last name.
    /// </summary>
    public string SenderLastName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the status.
    /// </summary>
    public int Status { get; init; }
}
