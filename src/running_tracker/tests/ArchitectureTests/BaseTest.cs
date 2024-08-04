using Domain.Abstractions;
using System.Reflection;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
}
