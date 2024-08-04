using Domain.Abstractions;
using Domain.Users;

namespace Domain.Followers;

public sealed class FollowerService
{
    private readonly IFollowerRepository _followerRepository;

    public FollowerService(IFollowerRepository followerRepository)
    {
        _followerRepository = followerRepository;
    }

    public async Task<Result> StartFollowingAsync(
        User user,
        User followed,
        DateTime utcNow,
        CancellationToken cancellationToken)
    {
        if (user.Id == followed.Id)
        {
            return FollowerErrors.SameUser;
        }

        if (!followed.HasPublicProfile)
        {
            return FollowerErrors.NonPublicProfile;
        }

        if (await _followerRepository.IsAlreadyFollowingAsync(
                user.Id,
                followed.Id,
                cancellationToken))
        {
            return FollowerErrors.AlreadyFollowing;
        }

        var follower = Follower.Create(user.Id, followed.Id, utcNow);

        _followerRepository.Insert(follower);

        return Result.Success();
    }
}