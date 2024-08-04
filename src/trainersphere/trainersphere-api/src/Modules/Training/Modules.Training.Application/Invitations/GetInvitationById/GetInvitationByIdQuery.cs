using Application.Messaging;

namespace Modules.Training.Application.Invitations.GetInvitationById;

/// <summary>
/// Represents the query for getting an invitation by identifier.
/// </summary>
/// <param name="InvitationId">The invitation identifier.</param>
public sealed record GetInvitationByIdQuery(Guid InvitationId) : IQuery<InvitationResponse>;
