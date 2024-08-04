using Domain.Primitives;

namespace Modules.Training.Domain.Workouts;

/// <summary>
/// Represents the rest interval type enumeration.
/// </summary>
public sealed class RestIntervalType : Enumeration<RestIntervalType>
{
    public static readonly RestIntervalType Seconds = new(1, "Seconds");
    public static readonly RestIntervalType Minutes = new(2, "Minutes");

    /// <summary>
    /// Initializes a new instance of the <see cref="RestIntervalType"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    private RestIntervalType(int id, string name)
        : base(id, name)
    {
    }
}
