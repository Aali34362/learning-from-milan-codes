namespace Authorization.Contracts;

/// <summary>
/// Represents the user permissions request interface.
/// </summary>
public interface IUserPermissionsRequest
{
    /// <summary>
    /// Gets the user identity provider identifier.
    /// </summary>
    string UserIdentityProviderId { get; }
}
