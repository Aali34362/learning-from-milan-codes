using Gatherly.Application.Abstractions;
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Invitations.SendInvitation;

internal sealed class SendInvitationCommandHandler : ICommandHandler<SendInvitationCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public SendInvitationCommandHandler(
        IMemberRepository memberRepository,
        IGatheringRepository gatheringRepository,
        IInvitationRepository invitationRepository,
        IUnitOfWork unitOfWork,
        IEmailService emailService)
    {
        _memberRepository = memberRepository;
        _gatheringRepository = gatheringRepository;
        _invitationRepository = invitationRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<Result> Handle(SendInvitationCommand request, CancellationToken cancellationToken) =>
        await Result.Combine(
                Result.Create(
                    await _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken)),
                Result.Create(
                    await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken)))
            .Bind(t => t.Item1.SendInvitation(t.Item2))
            .Tap(_invitationRepository.Add)
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .Tap(invitation => _emailService.SendInvitationSentEmailAsync(
                invitation.Member,
                invitation.Gathering,
                cancellationToken));

    public async Task<Unit> HandleOld(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        Member? member = await _memberRepository
            .GetByIdAsync(request.MemberId, cancellationToken);

        Gathering? gathering = await _gatheringRepository
            .GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if (member is null || gathering is null)
        {
            return Unit.Value;
        }

        Result<Invitation> invitationResult = gathering.SendInvitation(member);

        if (invitationResult.IsFailure)
        {
            // Log error
            return Unit.Value;
        }

        _invitationRepository.Add(invitationResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Send email
        await _emailService.SendInvitationSentEmailAsync(
            member,
            gathering,
            cancellationToken);

        return Unit.Value;
    }
}
