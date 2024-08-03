using System.Reflection;

namespace Infrastructure;

public static class InfrastructureAssembly
{
    public static readonly Assembly Instance = typeof(InfrastructureAssembly).Assembly;
}
