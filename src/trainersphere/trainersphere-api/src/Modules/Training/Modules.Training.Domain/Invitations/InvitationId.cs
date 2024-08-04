using Domain.Primitives;

namespace Modules.Training.Domain.Invitations;

/// <summary>
/// Represents the invitation identifier.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record InvitationId(Guid Value) : IEntityId;
