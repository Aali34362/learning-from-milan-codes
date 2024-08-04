using Gatherly.Application.Abstractions;

namespace Gatherly.Infrastructure.Services;

public sealed class SystemTimeProvider : ISystemTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
