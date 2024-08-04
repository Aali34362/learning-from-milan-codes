using Domain.Followers;

namespace Infrastructure.Repositories;

internal sealed class FollowerRepository : IFollowerRepository
{
    public Task<bool> IsAlreadyFollowingAsync(
        Guid userId,
        Guid followedId,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(false);
    }

    public void Insert(Follower follower)
    {
    }
}
