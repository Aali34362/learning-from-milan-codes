using Domain.Users;
using SharedKernel;

namespace Domain.Followers;

public interface IFollowerService
{
    Task<Result> StartFollowingAsync(
        User user,
        User followed,
        CancellationToken cancellationToken);
}
