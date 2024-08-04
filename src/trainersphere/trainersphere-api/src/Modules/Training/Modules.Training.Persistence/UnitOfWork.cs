using Application.ServiceLifetimes;
using Modules.Training.Domain;

namespace Modules.Training.Persistence;

/// <summary>
/// Represents the user's module unit of work.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork, IScoped
{
    private readonly TrainingDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UnitOfWork(TrainingDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}
