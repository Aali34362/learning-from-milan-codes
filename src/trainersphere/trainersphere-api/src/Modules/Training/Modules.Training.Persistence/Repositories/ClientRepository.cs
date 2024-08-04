using Application.ServiceLifetimes;
using Microsoft.EntityFrameworkCore;
using Modules.Training.Domain.Clients;
using Shared.Results;

namespace Modules.Training.Persistence.Repositories;

/// <summary>
/// Represents the client repository.
/// </summary>
internal sealed class ClientRepository : IClientRepository, IScoped
{
    private readonly TrainingDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public ClientRepository(TrainingDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<Result<Client>> GetByIdAsync(ClientId id, CancellationToken cancellationToken = default) =>
        Result.Create(await _dbContext.Set<Client>().FirstOrDefaultAsync(client => client.Id == id, cancellationToken));

    /// <inheritdoc />
    public async Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken) =>
        Result.Create(!await _dbContext.Set<Client>().AnyAsync(trainer => trainer.Email == email, cancellationToken));

    /// <inheritdoc />
    public void Add(Client client) => _dbContext.Set<Client>().Add(client);
}
