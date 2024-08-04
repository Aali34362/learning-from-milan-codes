using Application.ServiceLifetimes;
using Microsoft.EntityFrameworkCore;
using Modules.Training.Domain.Trainers;
using Shared.Results;

namespace Modules.Training.Persistence.Repositories;

/// <summary>
/// Represents the trainer repository.
/// </summary>
internal sealed class TrainerRepository : ITrainerRepository, IScoped
{
    private readonly TrainingDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrainerRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public TrainerRepository(TrainingDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<Result<Trainer>> GetByIdAsync(TrainerId id, CancellationToken cancellationToken = default) =>
        Result.Create(await _dbContext.Set<Trainer>().FirstOrDefaultAsync(user => user.Id == id, cancellationToken));

    /// <inheritdoc />
    public async Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken) =>
        Result.Create(!await _dbContext.Set<Trainer>().AnyAsync(trainer => trainer.Email == email, cancellationToken));

    /// <inheritdoc />
    public void Add(Trainer trainer) => _dbContext.Set<Trainer>().Add(trainer);
}
