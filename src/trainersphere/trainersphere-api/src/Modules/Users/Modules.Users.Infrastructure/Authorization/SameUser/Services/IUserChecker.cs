namespace Modules.Users.Infrastructure.Authorization.SameUser.Services;

/// <summary>
/// Represents the user checker interface.
/// </summary>
internal interface IUserChecker
{
    /// <summary>
    /// Checks if the user with the specified identifier and identity provider identifier exists.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="identityProviderId">The identity provider identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the user with the specified identifier and identity provider identifier exists, otherwise false.</returns>
    Task<bool> ExistsAsync(Guid userId, string identityProviderId, CancellationToken cancellationToken = default);
}
