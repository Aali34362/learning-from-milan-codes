using Application.ServiceLifetimes;
using Modules.Users.Domain;

namespace Modules.Users.Persistence;

/// <summary>
/// Represents the user's module unit of work.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork, IScoped
{
    private readonly UsersDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UnitOfWork(UsersDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}
