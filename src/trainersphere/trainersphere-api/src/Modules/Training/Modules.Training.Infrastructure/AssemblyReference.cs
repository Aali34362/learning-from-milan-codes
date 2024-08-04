using System.Reflection;

namespace Modules.Training.Infrastructure;

/// <summary>
/// Represents the training module infrastructure assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
