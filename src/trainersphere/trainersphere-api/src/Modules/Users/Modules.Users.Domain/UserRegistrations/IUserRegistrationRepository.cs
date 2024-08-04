using Shared.Results;

namespace Modules.Users.Domain.UserRegistrations;

/// <summary>
/// Represents the user registration repository interface.
/// </summary>
public interface IUserRegistrationRepository
{
    /// <summary>
    /// Gets the user registration with the specified identifier, if it exists.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user registration with the specified identifier if it exists, otherwise null.</returns>
    Task<Result<UserRegistration>> GetByIdAsync(UserRegistrationId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks that there are no pending invitations for the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The success result if there are no pending invitation for the specified email, otherwise a failure result.</returns>
    Task<Result> CheckNonePendingForEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the specified user registration to the repository.
    /// </summary>
    /// <param name="userRegistration">The user registration.</param>
    void Add(UserRegistration userRegistration);
}
