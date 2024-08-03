using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace Gatherly.Persistence.Repositories;

public class CachedMemberRepository : IMemberRepository
{
    private readonly IMemberRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CachedMemberRepository(IMemberRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string key = $"member-{id}";

        return _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _decorated.GetByIdAsync(id, cancellationToken);
            });
    }

    public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default) =>
        _decorated.IsEmailUniqueAsync(email, cancellationToken);

    public void Add(Member member) => _decorated.Add(member);

    public void Update(Member member) => _decorated.Update(member);
}
