using System.Reflection;

namespace Presentation;

public static class PresentationAssembly
{
    public static readonly Assembly Instance = typeof(PresentationAssembly).Assembly;
}
