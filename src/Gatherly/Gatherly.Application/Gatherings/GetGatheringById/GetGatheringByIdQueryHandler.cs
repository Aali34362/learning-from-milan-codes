﻿using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Gatherings.GetGatheringById;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Gatherings.GetGatheringById;

internal sealed class GetGatheringByIdQueryHandler
    : IQueryHandler<GetGatheringByIdQuery, GatheringResponse>
{
    private readonly IGatheringRepository _gatheringRepository;

    public GetGatheringByIdQueryHandler(IGatheringRepository gatheringRepository) =>
        _gatheringRepository = gatheringRepository;

    public async Task<Result<GatheringResponse>> Handle(
        GetGatheringByIdQuery request,
        CancellationToken cancellationToken)
    {
        Gathering? gathering = await _gatheringRepository.GetByIdAsync(
            request.GatheringId,
            cancellationToken);

        if (gathering is null)
        {
            return Result.Failure<GatheringResponse>(
                DomainErrors.Gathering.NotFound(request.GatheringId));
        }

        var response = new GatheringResponse(
            gathering.Id,
            gathering.Name,
            gathering.Location,
            $"{gathering.Creator.FirstName}" +
            $" {gathering.Creator.LastName}",
            gathering
                .Attendees
                .Select(attendee => new AttendeeResponse(
                    attendee.MemberId,
                    attendee.CreatedOnUtc))
                .ToList(),
            gathering
                .Invitations
                .Select(invitation => new InvitationResponse(
                    invitation.Id,
                    invitation.Status))
                .ToList());

        return response;
    }
}
