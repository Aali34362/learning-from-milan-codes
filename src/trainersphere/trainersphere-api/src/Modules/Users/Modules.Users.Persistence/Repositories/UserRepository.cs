using Application.ServiceLifetimes;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Users;
using Shared.Results;

namespace Modules.Users.Persistence.Repositories;

/// <summary>
/// Represents the user repository.
/// </summary>
internal sealed class UserRepository : IUserRepository, IScoped
{
    private readonly UsersDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UserRepository(UsersDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<Result<User>> GetByIdAsync(UserId id, CancellationToken cancellationToken = default) =>
        Result.Create(await _dbContext.Set<User>().Include(user => user.Roles).FirstOrDefaultAsync(user => user.Id == id, cancellationToken));

    /// <inheritdoc />
    public async Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default) =>
        Result.Create(!await _dbContext.Set<User>().AnyAsync(user => user.Email == email, cancellationToken));

    /// <inheritdoc />
    public void Add(User user)
    {
        _dbContext.Set<User>().Add(user);

        _dbContext.AttachRange(user.Roles);
    }
}
