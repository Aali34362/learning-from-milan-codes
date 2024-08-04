using Domain.Primitives;

namespace Modules.Users.Domain.UserRegistrations;

/// <summary>
/// Represents the user registration status enumeration.
/// </summary>
public sealed class UserRegistrationStatus : Enumeration<UserRegistrationStatus>
{
    public static readonly UserRegistrationStatus Pending = new(1, "Pending");
    public static readonly UserRegistrationStatus Confirmed = new(2, "Confirmed");
    public static readonly UserRegistrationStatus Cancelled = new(3, "Cancelled");
    public static readonly UserRegistrationStatus Expired = new(4, "Expired");

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegistrationStatus"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    private UserRegistrationStatus(int id, string name)
        : base(id, name)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegistrationStatus"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private UserRegistrationStatus(int id)
    {
        UserRegistrationStatus? userRegistrationStatus = FromId(id);

        Id = userRegistrationStatus?.Id ?? throw new ArgumentNullException(nameof(id));
        Name = userRegistrationStatus?.Name ?? throw new ArgumentNullException(nameof(id));
    }
}
