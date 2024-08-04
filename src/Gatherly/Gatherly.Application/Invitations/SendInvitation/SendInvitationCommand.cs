using Gatherly.Application.Abstractions.Messaging;

namespace Gatherly.Application.Invitations.SendInvitation;

public sealed record SendInvitationCommand(Guid MemberId, Guid GatheringId) : ICommand;
