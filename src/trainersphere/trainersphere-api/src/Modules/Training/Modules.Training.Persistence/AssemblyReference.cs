using System.Reflection;

namespace Modules.Training.Persistence;

/// <summary>
/// Represents the training module persistence assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
