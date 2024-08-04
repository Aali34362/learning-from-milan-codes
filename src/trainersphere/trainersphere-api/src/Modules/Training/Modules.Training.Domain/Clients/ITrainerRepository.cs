using Shared.Results;

namespace Modules.Training.Domain.Clients;

/// <summary>
/// Represents the client repository interface.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Gets the client with the specified identifier, if it exists.
    /// </summary>
    /// <param name="id">The client identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The client with the specified identifier if it exists, otherwise null.</returns>
    Task<Result<Client>> GetByIdAsync(ClientId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the specified email is unique.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The success result if the email is unique, otherwise a failure result.</returns>
    Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified client to the repository.
    /// </summary>
    /// <param name="client">The client.</param>
    void Add(Client client);
}
