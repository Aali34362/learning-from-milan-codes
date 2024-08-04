namespace Authorization.Services;

/// <summary>
/// Represents the permission service interface.
/// </summary>
internal interface IPermissionService
{
    /// <summary>
    /// Gets the permissions for the user with the specified user identifier.
    /// </summary>
    /// <param name="identityProviderId">The identity provider identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The permission for the user with the specified identifier.</returns>
    Task<HashSet<string>> GetPermissionsAsync(string identityProviderId, CancellationToken cancellationToken = default);
}
