using Application.Messaging;

namespace Modules.Training.Application.Invitations.CancelInvitation;

/// <summary>
/// Represents the command for cancelling an invitation.
/// </summary>
/// <param name="InvitationId">The invitation identifier.</param>
public sealed record CancelInvitationCommand(Guid InvitationId) : ICommand;
