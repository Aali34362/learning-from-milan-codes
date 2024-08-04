using Shared.Errors;

namespace Modules.Training.Domain.Trainers;

/// <summary>
/// Contains the trainer errors.
/// </summary>
public static class TrainerErrors
{
    /// <summary>
    /// Gets the not found error.
    /// </summary>
    public static Func<TrainerId, Error> NotFound => trainerId => new NotFoundError(
        "Trainer.NotFound",
        $"The trainer with the identifier '{trainerId.Value}' was not found.");
}
