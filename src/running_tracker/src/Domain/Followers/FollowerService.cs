using Domain.Users;

namespace Domain.Followers;

public sealed class FollowerService
{
    private readonly IFollowerRepository _followerRepository;

    public FollowerService(IFollowerRepository followerRepository)
    {
        _followerRepository = followerRepository;
    }

    public async Task StartFollowingAsync(
        User user,
        User followed,
        DateTime createdOnUtc,
        CancellationToken cancellationToken)
    {
        if (user.Id == followed.Id)
        {
            throw new Exception("Can't follow yourself");
        }

        if (!followed.HasPublicProfile)
        {
            throw new Exception("Can't follow non-public profile");
        }

        if (await _followerRepository.IsAlreadyFollowingAsync(
                user.Id,
                followed.Id,
                cancellationToken))
        {
            throw new Exception("Already following");
        }

        var follower = Follower.Create(user.Id, followed.Id, createdOnUtc);

        _followerRepository.Insert(follower);
    }
}