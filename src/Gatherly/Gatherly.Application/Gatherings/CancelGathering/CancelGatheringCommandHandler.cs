using Gatherly.Application.Abstractions;
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Gatherings.CancelGathering;

internal sealed class CancelGatheringCommandHandler : ICommandHandler<CancelGatheringCommand>
{
    private readonly IGatheringRepository _gatheringRepository;
    private readonly ISystemTimeProvider _systemTimeProvider;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public CancelGatheringCommandHandler(
        IGatheringRepository gatheringRepository,
        ISystemTimeProvider systemTimeProvider,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _gatheringRepository = gatheringRepository;
        _systemTimeProvider = systemTimeProvider;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CancelGatheringCommand request, CancellationToken cancellationToken)
    {
        Gathering? gathering = await _gatheringRepository.GetByIdAsync(
            request.GatheringId,
            cancellationToken);

        if (gathering is null)
        {
            return Result.Failure(DomainErrors.Gathering.NotFound(request.GatheringId));
        }

        Result result =  gathering.Cancel(_systemTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
