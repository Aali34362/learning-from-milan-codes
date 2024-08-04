using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

/// <summary>
/// Represents the exercise entity.
/// </summary>
public sealed class Exercise : Entity<ExerciseId>, IAuditable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Exercise"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    /// <param name="description">Th description.</param>
    public Exercise(ExerciseId id, string name, string? description = null)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description, if it exists.
    /// </summary>
    public string? Description { get; private set; }

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; private set; }
}
