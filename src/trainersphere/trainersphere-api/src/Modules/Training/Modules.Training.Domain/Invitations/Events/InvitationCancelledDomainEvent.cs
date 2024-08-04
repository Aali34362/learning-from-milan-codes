using Domain.Primitives;

namespace Modules.Training.Domain.Invitations.Events;

/// <summary>
/// Represents the domain event that is raised when an invitation is cancelled.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="OccurredOnUtc">The occurred on date and time.</param>
/// <param name="InvitationId">The invitation identifier.</param>
public sealed record InvitationCancelledDomainEvent(Guid Id, DateTime OccurredOnUtc, InvitationId InvitationId) : DomainEvent(Id, OccurredOnUtc);
