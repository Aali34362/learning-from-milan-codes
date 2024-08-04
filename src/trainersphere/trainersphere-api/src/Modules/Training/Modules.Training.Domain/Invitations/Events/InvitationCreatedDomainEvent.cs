using Domain.Primitives;
using Modules.Training.Domain.Trainers;

namespace Modules.Training.Domain.Invitations.Events;

/// <summary>
/// Represents the domain event that is raised when a client invitation is created.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="OccurredOnUtc">The occurred on date and time.</param>
/// <param name="InvitationId">The invitation identifier.</param>
/// <param name="TrainerId">The trainer identifier.</param>
/// <param name="Email">The email.</param>
public sealed record InvitationCreatedDomainEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    InvitationId InvitationId,
    TrainerId TrainerId,
    string Email,
    Sender Sender) : DomainEvent(Id, OccurredOnUtc);
