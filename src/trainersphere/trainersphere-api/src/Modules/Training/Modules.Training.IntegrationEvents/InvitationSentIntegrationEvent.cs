using Application.EventBus;

namespace Modules.Training.IntegrationEvents;

/// <summary>
/// Represents the invitation sent integration event.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="OccurredOnUtc">The occurred on date and time.</param>
/// <param name="InvitationId">The invitation identifier.</param>
/// <param name="TrainerId">The trainer identifier.</param>
/// <param name="Email">The email.</param>
/// <param name="SenderFirstName">The sender first name.</param>
/// <param name="SenderLastName">The sender last name.</param>
public sealed record InvitationSentIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid InvitationId,
    Guid TrainerId,
    string Email,
    string SenderFirstName,
    string SenderLastName) : IntegrationEvent(Id, OccurredOnUtc);
