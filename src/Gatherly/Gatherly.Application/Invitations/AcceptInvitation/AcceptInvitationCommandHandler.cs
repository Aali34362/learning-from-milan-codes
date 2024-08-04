using Gatherly.Domain.Entities;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Invitations.AcceptInvitation;

internal sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand>
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

    public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository
            .GetByIdWithInvitationsAsync(request.GatheringId, cancellationToken);

        if (gathering is null)
        {
            return Unit.Value;
        }

        var invitation = gathering.Invitations
            .FirstOrDefault(i => i.Id == request.InvitationId);

        if (invitation is null || invitation.Status != InvitationStatus.Pending)
        {
            return Unit.Value;
        }

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            Result<Attendee> attendeeResult = gathering.AcceptInvitation(invitation);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (attendeeResult.IsSuccess)
            {
                _attendeeRepository.Add(attendeeResult.Value);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }

        return Unit.Value;
    }
}
