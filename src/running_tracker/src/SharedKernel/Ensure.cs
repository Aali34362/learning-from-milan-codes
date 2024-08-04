using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SharedKernel;

public static class Ensure
{
    public static void NotNullOrEmpty(
        [NotNull] string? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName);
        }
    }

    public static void NotNull(
        [NotNull] object? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    public static void GreaterThanZero(
        decimal value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName);
        }
    }
}
