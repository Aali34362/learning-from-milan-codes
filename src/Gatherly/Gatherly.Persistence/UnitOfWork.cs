using Gatherly.Domain.Repositories;

namespace Gatherly.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}