using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;

namespace Gatherly.Persistence.Repository;

internal sealed class GatheringRepository : IGatheringRepository
{
    public async Task<Gathering?> GetByIdWithCreatorAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        return null;
    }

    public void Add(Gathering gathering)
    {
    }
}