using Application.Messaging;

namespace Modules.Training.Application.Invitations.GetInvitations;

/// <summary>
/// Represents the query for getting a list of invitations.
/// </summary>
/// <param name="TrainerId">The trainer identifier.</param>
public sealed record GetInvitationsQuery(Guid TrainerId) : IQuery<List<InvitationResponse>>;
