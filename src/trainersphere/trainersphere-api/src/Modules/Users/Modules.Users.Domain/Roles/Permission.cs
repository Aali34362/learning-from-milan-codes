using Domain.Primitives;

namespace Modules.Users.Domain.Roles;

/// <summary>
/// Represents the permission enumeration.
/// </summary>
public sealed class Permission : Enumeration<Permission>
{
    public static readonly Permission ReadUser = new(100, nameof(ReadUser), "Can read user information.");
    public static readonly Permission ModifyUser = new(101, nameof(ModifyUser), "Can modify user information.");

    public static readonly Permission ReadInvitations = new(200, nameof(ReadInvitations), "Can read invitations.");
    public static readonly Permission CancelInvitation = new(201, nameof(CancelInvitation), "Can cancel invitation.");
    public static readonly Permission InviteClient = new(202, nameof(InviteClient), "Can invite new client.");

    /// <summary>
    /// Initializes a new instance of the <see cref="Permission"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    /// <param name="description">The description.</param>
    private Permission(int id, string name, string description)
        : base(id, name) =>
        Description = description;

    /// <summary>
    /// Initializes a new instance of the <see cref="Permission"/> class.
    /// </summary>
    private Permission() => Description = string.Empty;

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; private init; }
}
