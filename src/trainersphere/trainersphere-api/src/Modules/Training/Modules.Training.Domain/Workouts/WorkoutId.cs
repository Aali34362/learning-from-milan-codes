using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

/// <summary>
/// Represents the workout identifier.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record WorkoutId(Guid Value) : IEntityId;
