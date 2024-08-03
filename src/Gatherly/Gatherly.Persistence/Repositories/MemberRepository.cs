using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;

namespace Gatherly.Persistence.Repositories;

internal sealed class MemberRepository : IMemberRepository
{
    public Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Add(Member member)
    {
        throw new NotImplementedException();
    }
}