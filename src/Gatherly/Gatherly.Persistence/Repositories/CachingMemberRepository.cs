using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using Gatherly.Persistence.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace Gatherly.Persistence.Repositories;

public sealed class CachingMemberRepository : IMemberRepository
{
    private static readonly TimeSpan CacheTime = TimeSpan.FromMinutes(2);
    private readonly IMemberRepository _memberRepository;
    private readonly IMemoryCache _memoryCache;

    public CachingMemberRepository(IMemberRepository memberRepository, IMemoryCache memoryCache)
    {
        _memberRepository = memberRepository;
        _memoryCache = memoryCache;
    }

    public Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _memoryCache.GetOrCreateAsync(
            CacheKeys.MemberById(id),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);

                // Attach?
                return _memberRepository.GetByIdAsync(id, cancellationToken);
            });
    }

    public Task<Member?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return _memoryCache.GetOrCreateAsync(
            CacheKeys.MemberByEmail(email),
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(CacheTime);

                return _memberRepository.GetByEmailAsync(email, cancellationToken);
            });
    }

    public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        return _memberRepository.IsEmailUniqueAsync(email, cancellationToken);
    }

    public void Add(Member member)
    {
        _memberRepository.Add(member);
    }

    public void Update(Member member)
    {
        _memberRepository.Update(member);
    }
}
