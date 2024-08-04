namespace Modules.Users.Domain.Roles;

/// <summary>
/// Represents the role-permission join entity.
/// </summary>
public sealed class RolePermission
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RolePermission"/> class.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <param name="permission">The permission.</param>
    public RolePermission(Role role, Permission permission)
    {
        RoleId = role.Id;
        PermissionId = permission.Id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RolePermission"/> class.
    /// </summary>
    /// <remarks>
    ///  Required by EF Core.
    /// </remarks>
    private RolePermission()
    {
    }

    /// <summary>
    /// Gets the role identifier.
    /// </summary>
    public int RoleId { get; private set; }

    /// <summary>
    /// Gets the permission identifier.
    /// </summary>
    public int PermissionId { get; private set; }
}
