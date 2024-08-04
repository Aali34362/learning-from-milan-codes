namespace Gatherly.Application.Abstractions;

public interface ISystemTimeProvider
{
    public DateTime UtcNow { get; }
}
