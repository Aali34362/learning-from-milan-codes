using Gatherly.Application.Abstractions.Messaging;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation;

public sealed record AcceptInvitationCommand(Guid GatheringId, Guid InvitationId) : ICommand;