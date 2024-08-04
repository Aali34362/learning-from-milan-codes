using Domain.Users;

namespace Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(default(User?));
    }

    public Task<bool> IsEmailUniqueAsync(Email email)
    {
        throw new ApplicationException("Repository exception");
    }

    public void Insert(User user)
    {
    }
}
