using Application.Messaging;
using Modules.Training.Domain;
using Modules.Training.Domain.Clients;
using Modules.Training.Domain.Invitations;
using Modules.Training.Domain.Trainers;
using Shared.Results;

namespace Modules.Training.Application.Trainers.InviteClient;

/// <summary>
/// Represents the <see cref="InviteClientCommand"/> class.
/// </summary>
internal sealed class InviteClientCommandHandler : ICommandHandler<InviteClientCommand, Guid>
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="InviteClientCommandHandler"/> class.
    /// </summary>
    /// <param name="trainerRepository">The trainer repository.</param>
    /// <param name="clientRepository">The client repository.</param>
    /// <param name="invitationRepository">The invitation repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public InviteClientCommandHandler(
        ITrainerRepository trainerRepository,
        IClientRepository clientRepository,
        IInvitationRepository invitationRepository,
        IUnitOfWork unitOfWork)
    {
        _trainerRepository = trainerRepository;
        _clientRepository = clientRepository;
        _invitationRepository = invitationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(InviteClientCommand request, CancellationToken cancellationToken) =>
        await GetTrainerByIdAsync(new TrainerId(request.TrainerId), cancellationToken)
            .Bind(trainer => trainer.Invite(request.Email))
            .Bind(invitation => CheckIfEmailIsUniqueAsync(invitation, cancellationToken))
            .Bind(invitation => CheckNoPendingInvitationsAsync(invitation, cancellationToken))
            .Tap(invitation => _invitationRepository.Add(invitation))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .Map(invitation => invitation.Id.Value);

    private async Task<Result<Trainer>> GetTrainerByIdAsync(TrainerId trainerId, CancellationToken cancellationToken) =>
        await _trainerRepository.GetByIdAsync(trainerId, cancellationToken)
            .MapFailure(() => TrainerErrors.NotFound(trainerId));

    private async Task<Result<Invitation>> CheckIfEmailIsUniqueAsync(Invitation invitation, CancellationToken cancellationToken) =>
        await Result.FirstFailureOrSuccess(
                () => _trainerRepository.IsEmailUniqueAsync(invitation.Email, cancellationToken),
                () => _clientRepository.IsEmailUniqueAsync(invitation.Email, cancellationToken))
            .Map(() => invitation)
            .MapFailure(() => InvitationErrors.EmailIsNotUnique);

    private async Task<Result<Invitation>> CheckNoPendingInvitationsAsync(Invitation invitation, CancellationToken cancellationToken) =>
        await _invitationRepository.CheckNonePendingForEmailAsync(invitation.Email, cancellationToken)
            .Map(() => invitation)
            .MapFailure(() => InvitationErrors.PendingExists);
}
