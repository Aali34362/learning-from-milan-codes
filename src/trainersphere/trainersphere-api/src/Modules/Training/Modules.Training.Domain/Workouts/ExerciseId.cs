using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

/// <summary>
/// Represents the exercise identifier.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record ExerciseId(Guid Value) : IEntityId;
