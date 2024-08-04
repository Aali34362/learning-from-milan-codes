using Domain.Primitives;

namespace Modules.Training.Domain.Trainers;

/// <summary>
/// Represents the trainer identifier.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record TrainerId(Guid Value) : IEntityId;
