using Domain.Primitives;
using Shared.Results;

namespace Modules.Training.Domain.Workouts;

/// <summary>
/// Represents the goal value object.
/// </summary>
public sealed class Goal : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Goal"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    private Goal(GoalType type, string value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Gets the type.
    /// </summary>
    public GoalType Type { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates a new goal instance based on the specified parameters.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    /// <returns>The newly created goal instance.</returns>
    public static Result<Goal> Create(GoalType type, string value) => Result.Success(new Goal(type, value));

    /// <inheritdoc />
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Type.Id;
        yield return Value;
    }
}
