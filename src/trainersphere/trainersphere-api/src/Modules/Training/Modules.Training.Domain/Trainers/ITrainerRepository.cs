using Shared.Results;

namespace Modules.Training.Domain.Trainers;

/// <summary>
/// Represents the trainer repository interface.
/// </summary>
public interface ITrainerRepository
{
    /// <summary>
    /// Gets the trainer with the specified identifier, if it exists.
    /// </summary>
    /// <param name="id">The trainer identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The trainer with the specified identifier if it exists, otherwise null.</returns>
    Task<Result<Trainer>> GetByIdAsync(TrainerId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the specified email is unique.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The success result if the email is unique, otherwise a failure result.</returns>
    Task<Result> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified trainer to the repository.
    /// </summary>
    /// <param name="trainer">The trainer.</param>
    void Add(Trainer trainer);
}
