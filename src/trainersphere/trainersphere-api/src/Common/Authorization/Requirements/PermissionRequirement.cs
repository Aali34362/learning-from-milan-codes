using Microsoft.AspNetCore.Authorization;

namespace Authorization.Requirements;

/// <summary>
/// Represents the permission authorization requirement.
/// </summary>
internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionRequirement"/> class.
    /// </summary>
    /// <param name="permission">The permission.</param>
    internal PermissionRequirement(string permission) => Permission = permission;

    /// <summary>
    /// Gets the permission.
    /// </summary>
    internal string Permission { get; }
}
