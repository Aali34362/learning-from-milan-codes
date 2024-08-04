using System.Reflection;

namespace Modules.Notifications.Persistence;

/// <summary>
/// Represents the notifications module persistence assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
