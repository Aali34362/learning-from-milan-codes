using System.Reflection;

namespace Modules.Notifications.Application;

/// <summary>
/// Represents the notifications module application assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
