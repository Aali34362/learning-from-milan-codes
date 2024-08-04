using System.Reflection;

namespace Modules.Notifications.Infrastructure;

/// <summary>
/// Represents the notifications module infrastructure assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
