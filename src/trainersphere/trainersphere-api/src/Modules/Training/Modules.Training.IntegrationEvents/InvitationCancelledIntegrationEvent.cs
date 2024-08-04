using Application.EventBus;

namespace Modules.Training.IntegrationEvents;

/// <summary>
/// Represents the invitation cancelled integration event.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="OccurredOnUtc">The occurred on date and time.</param>
/// <param name="InvitationId">The invitation identifier.</param>
public sealed record InvitationCancelledIntegrationEvent(Guid Id, DateTime OccurredOnUtc, Guid InvitationId) : IntegrationEvent(Id, OccurredOnUtc);
