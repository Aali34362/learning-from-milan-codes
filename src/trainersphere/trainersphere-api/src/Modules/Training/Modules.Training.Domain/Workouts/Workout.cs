using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

/// <summary>
/// Represents the workout entity.
/// </summary>
public sealed class Workout : Entity<WorkoutId>, IAuditable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Workout"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    public Workout(WorkoutId id, string name)
        : base(id) =>
        Name = name;

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; private set; }

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; private set; }
}
