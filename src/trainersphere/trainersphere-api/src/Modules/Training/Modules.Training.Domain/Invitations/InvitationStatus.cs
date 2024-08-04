using Domain.Primitives;

namespace Modules.Training.Domain.Invitations;

/// <summary>
/// Represents the invitation status enumeration.
/// </summary>
public sealed class InvitationStatus : Enumeration<InvitationStatus>
{
    public static readonly InvitationStatus Pending = new(1, "Pending");
    public static readonly InvitationStatus Accepted = new(2, "Accepted");
    public static readonly InvitationStatus Cancelled = new(3, "Cancelled");
    public static readonly InvitationStatus Expired = new(4, "Expired");

    /// <summary>
    /// Initializes a new instance of the <see cref="InvitationStatus"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    private InvitationStatus(int id, string name)
        : base(id, name)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvitationStatus"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private InvitationStatus(int id)
    {
        InvitationStatus? userRegistrationStatus = FromId(id);

        Id = userRegistrationStatus?.Id ?? throw new ArgumentNullException(nameof(id));
        Name = userRegistrationStatus?.Name ?? throw new ArgumentNullException(nameof(id));
    }
}
