using Application.Messaging;

namespace Modules.Training.Application.Trainers.InviteClient;

/// <summary>
/// Invites the client with the specified email to train with the trainer with the specified identifier.
/// </summary>
/// <param name="TrainerId">The trainer identifier.</param>
/// <param name="Email">The client email.</param>
public sealed record InviteClientCommand(Guid TrainerId, string Email) : ICommand<Guid>;
