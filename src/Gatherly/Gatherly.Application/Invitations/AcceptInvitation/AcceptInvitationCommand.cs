﻿using MediatR;

namespace Gatherly.Application.Invitations.AcceptInvitation;

public sealed record AcceptInvitationCommand(Guid GatheringId, Guid InvitationId) : IRequest;