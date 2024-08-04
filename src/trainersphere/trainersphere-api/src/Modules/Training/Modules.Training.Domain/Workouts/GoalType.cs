using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

/// <summary>
/// Represents the goal type enumeration.
/// </summary>
public sealed class GoalType : Enumeration<GoalType>
{
    public static readonly GoalType Weight = new(1, "Weight");
    public static readonly GoalType Time = new(2, "Time");
    public static readonly GoalType Distance = new(2, "Distance");

    /// <summary>
    /// Initializes a new instance of the <see cref="GoalType"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    private GoalType(int id, string name)
        : base(id, name)
    {
    }
}
