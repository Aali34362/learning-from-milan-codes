using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repository;

internal sealed class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MemberRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<Member>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default) =>
        !await _dbContext.Set<Member>().AnyAsync(x => x.Email == email, cancellationToken);

    public void Add(Member member) => _dbContext.Set<Member>().Add(member);
}
