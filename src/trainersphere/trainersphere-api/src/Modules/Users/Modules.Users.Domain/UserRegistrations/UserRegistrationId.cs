using Domain.Primitives;

namespace Modules.Users.Domain.UserRegistrations;

/// <summary>
/// Represents the user registration identifier.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record UserRegistrationId(Guid Value) : IEntityId;
