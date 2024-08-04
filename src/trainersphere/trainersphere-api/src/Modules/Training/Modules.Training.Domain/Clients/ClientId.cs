using Domain.Primitives;

namespace Modules.Training.Domain.Clients;

/// <summary>
/// Represents the client identifier.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record ClientId(Guid Value) : IEntityId;
