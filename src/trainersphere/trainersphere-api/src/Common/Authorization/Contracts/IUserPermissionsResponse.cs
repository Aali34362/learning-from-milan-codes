namespace Authorization.Contracts;

/// <summary>
/// Represents the user permissions response interface.
/// </summary>
public interface IUserPermissionsResponse
{
    /// <summary>
    /// Gets the permissions.
    /// </summary>
    HashSet<string> Permissions { get; }
}
