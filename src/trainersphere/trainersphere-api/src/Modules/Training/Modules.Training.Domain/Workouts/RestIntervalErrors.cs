using Shared.Errors;

namespace Modules.Training.Domain.Workouts;

/// <summary>
/// Contains the rest interval errors.
/// </summary>
public static class RestIntervalErrors
{
    /// <summary>
    /// Gets the invalid error.
    /// </summary>
    public static Error Invalid => new("RestInterval.Invalid", "The provided value is not a valid rest interval");
}
