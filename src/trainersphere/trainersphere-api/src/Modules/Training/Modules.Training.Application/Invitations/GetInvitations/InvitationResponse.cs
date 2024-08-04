namespace Modules.Training.Application.Invitations.GetInvitations;

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
    /// Gets the status.
    /// </summary>
    public int Status { get; init; }

    /// <summary>
    /// Gets the created on date and time.
    /// </summary>
    public DateTime CreatedOnUtc { get; init; }

    /// <summary>
    /// Gets the modified on date and time.
    /// </summary>
    public DateTime? ModifiedOnUtc { get; init; }
}
