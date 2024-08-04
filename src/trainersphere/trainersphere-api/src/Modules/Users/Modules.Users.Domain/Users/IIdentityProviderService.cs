using Shared.Results;

namespace Modules.Users.Domain.Users;

/// <summary>
/// Represents the identity provider service.
/// </summary>
public interface IIdentityProviderService
{
    /// <summary>
    /// Checks if the user with the specified identity provider identifier exists.
    /// </summary>
    /// <param name="identityProviderId">The identity provider identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The success result if the user with the specified identity provider identifier exists, otherwise a failure result.</returns>
    Task<Result> ExistsAsync(string identityProviderId, CancellationToken cancellationToken = default);
}
