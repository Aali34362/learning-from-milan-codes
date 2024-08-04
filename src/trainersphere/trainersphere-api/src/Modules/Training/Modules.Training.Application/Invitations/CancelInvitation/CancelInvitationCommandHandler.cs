using Application.Messaging;
using Modules.Training.Domain;
using Modules.Training.Domain.Invitations;
using Shared.Results;

namespace Modules.Training.Application.Invitations.CancelInvitation;

/// <summary>
/// Represents the <see cref="CancelInvitationCommand"/> handler.
/// </summary>
internal sealed class CancelInvitationCommandHandler : ICommandHandler<CancelInvitationCommand>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelInvitationCommandHandler"/> class.
    /// </summary>
    /// <param name="invitationRepository">The invitation repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public CancelInvitationCommandHandler(IInvitationRepository invitationRepository, IUnitOfWork unitOfWork)
    {
        _invitationRepository = invitationRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(CancelInvitationCommand request, CancellationToken cancellationToken) =>
        await GetInvitationById(new InvitationId(request.InvitationId), cancellationToken)
            .Bind(invitation => invitation.Cancel())
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken));

    private async Task<Result<Invitation>> GetInvitationById(InvitationId invitationId, CancellationToken cancellationToken) =>
        await _invitationRepository.GetByIdAsync(invitationId, cancellationToken)
            .MapFailure(() => InvitationErrors.NotFound(invitationId));
}
