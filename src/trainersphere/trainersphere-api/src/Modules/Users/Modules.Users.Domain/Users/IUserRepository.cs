using Shared.Results;

namespace Modules.Users.Domain.Users;

/// <summary>
/// Represents the user repository interface.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets the user with the specified identifier, if it exists.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user with the specified identifier if it exists, otherwise null.</returns>
    Task<Result<User>> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the specified email is unique.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The success result if the email is unique, otherwise a failure result.</returns>
    Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the specified user to the repository.
    /// </summary>
    /// <param name="user">The user.</param>
    void Add(User user);
}
