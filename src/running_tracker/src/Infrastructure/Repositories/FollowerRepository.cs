using Domain.Followers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class FollowerRepository : IFollowerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FollowerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsAlreadyFollowingAsync(
        Guid userId,
        Guid followedId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Followers.AnyAsync(f =>
            f.UserId == userId && f.FollowedId == followedId,
            cancellationToken);
    }

    public void Insert(Follower follower)
    {
        _dbContext.Followers.Add(follower);
    }
}
