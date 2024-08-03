using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;

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

    public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}