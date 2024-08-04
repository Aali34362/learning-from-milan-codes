using Domain.Primitives;

namespace Modules.Users.Domain.Users;

/// <summary>
/// Represents the user identifier.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record UserId(Guid Value) : IEntityId;
