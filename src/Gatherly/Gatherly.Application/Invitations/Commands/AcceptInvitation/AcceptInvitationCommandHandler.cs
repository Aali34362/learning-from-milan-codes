using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation;

internal sealed class AcceptInvitationCommandHandler : ICommandHandler<AcceptInvitationCommand>
{
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptInvitationCommandHandler(
        IGatheringRepository gatheringRepository,
        IAttendeeRepository attendeeRepository,
        IUnitOfWork unitOfWork)
    {
        _gatheringRepository = gatheringRepository;
        _attendeeRepository = attendeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository
            .GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if (gathering is null)
        {
            return Result.Failure(
                DomainErrors.Gathering.NotFound(request.GatheringId));
        }

        var invitation = gathering.Invitations
            .First(i => i.Id == request.InvitationId);

        if (invitation.Status != InvitationStatus.Pending)
        {
            return Result.Failure(
                DomainErrors.Invitation.AlreadyAccepted(invitation.Id));
        }

        var acceptInvitationResult = gathering.AcceptInvitation(invitation);

        if (acceptInvitationResult.IsSuccess)
        {
            _attendeeRepository.Add(acceptInvitationResult.Value);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}