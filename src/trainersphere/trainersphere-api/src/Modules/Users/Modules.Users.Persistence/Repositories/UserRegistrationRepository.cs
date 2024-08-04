using Application.ServiceLifetimes;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.UserRegistrations;
using Shared.Results;

namespace Modules.Users.Persistence.Repositories;

/// <summary>
/// Represents the user registration repository.
/// </summary>
internal sealed class UserRegistrationRepository : IUserRegistrationRepository, IScoped
{
    private readonly UsersDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegistrationRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UserRegistrationRepository(UsersDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<Result<UserRegistration>> GetByIdAsync(UserRegistrationId id, CancellationToken cancellationToken = default) =>
        Result.Create(
            await _dbContext.Set<UserRegistration>()
                .FirstOrDefaultAsync(userRegistration => userRegistration.Id == id, cancellationToken));

    /// <inheritdoc />
    public async Task<Result> CheckNonePendingForEmailAsync(string email, CancellationToken cancellationToken = default) =>
        Result.Create(
            !await _dbContext.Set<UserRegistration>()
                .AnyAsync(
                    userRegistration => userRegistration.Email == email && userRegistration.Status == UserRegistrationStatus.Pending,
                    cancellationToken));

    /// <inheritdoc />
    public void Add(UserRegistration userRegistration) => _dbContext.Set<UserRegistration>().Add(userRegistration);
}
