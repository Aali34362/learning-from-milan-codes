using Domain.Primitives;
using Shared.Results;

namespace Modules.Training.Domain.Workouts;

/// <summary>
/// Represents the rest interval value object.
/// </summary>
public sealed class RestInterval : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RestInterval"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    private RestInterval(RestIntervalType type, int value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Gets the type.
    /// </summary>
    public RestIntervalType Type { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Creates a new rest interval instance based on the specified parameters.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    /// <returns>The newly created rest interval instance.</returns>
    public static Result<RestInterval> Create(RestIntervalType type, int value) =>
        value >= 0 ?
            Result.Success(new RestInterval(type, value)) :
            Result.Failure<RestInterval>(RestIntervalErrors.Invalid);

    /// <inheritdoc />
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Type.Id;
        yield return Value;
    }
}
