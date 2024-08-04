using Shared.Errors;

namespace Modules.Training.Domain.Invitations;

/// <summary>
/// Contains the invitation errors.
/// </summary>
public static class InvitationErrors
{
    /// <summary>
    /// Gets the email is not unique error.
    /// </summary>
    public static Error EmailIsNotUnique => new ConflictError("Invitation.EmailIsNotUnique", "The specified email is already in use.");

    /// <summary>
    /// Gets the pending exists error.
    /// </summary>
    public static Error PendingExists => new ConflictError("Invitation.PendingExists", "There is already an existing invitation for the client.");

    /// <summary>
    /// Gets the email does not match error.
    /// </summary>
    public static Error EmailDoesNotMatch => new("Invitation.EmailDoesNotMatch", "There specified email does not match the invitation's email.");

    /// <summary>
    /// Gets the not found error.
    /// </summary>
    public static Func<InvitationId, Error> NotFound => invitationId => new NotFoundError(
        "Invitation.NotFound",
        $"The invitation with the identifier '{invitationId.Value}' was not found.");

    /// <summary>
    /// Gets the expired error.
    /// </summary>
    public static Func<InvitationId, Error> Expired => invitationId => new Error(
        "Invitation.Expired",
        $"The invitation with the identifier '{invitationId.Value}' has expired.");

    /// <summary>
    /// Gets the cancelled error.
    /// </summary>
    public static Func<InvitationId, Error> Cancelled => invitationId => new Error(
        "Invitation.Cancelled",
        $"The invitation with the identifier '{invitationId.Value}' has been cancelled.");
}
