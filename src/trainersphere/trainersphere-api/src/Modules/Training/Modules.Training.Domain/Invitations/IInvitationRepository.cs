using Shared.Results;

namespace Modules.Training.Domain.Invitations;

/// <summary>
/// Represents the invitation repository interface.
/// </summary>
public interface IInvitationRepository
{
    /// <summary>
    /// Gets the invitation with the specified identifier, if it exists.
    /// </summary>
    /// <param name="id">The invitation identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The invitation with the specified identifier if it exists, otherwise null.</returns>
    Task<Result<Invitation>> GetByIdAsync(InvitationId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks that there are no pending invitations for the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The success result if there are no pending invitation for the specified email, otherwise a failure result.</returns>
    Task<Result> CheckNonePendingForEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the specified invitation to the repository.
    /// </summary>
    /// <param name="invitation">The invitation.</param>
    void Add(Invitation invitation);
}
