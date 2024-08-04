using Application.EventBus;
using Application.Extensions;
using Microsoft.Extensions.Logging;
using Modules.Training.IntegrationEvents;
using Modules.Users.Domain;
using Modules.Users.Domain.UserRegistrations;
using Shared.Results;

namespace Modules.Users.Application.UserRegistrations.Events;

/// <summary>
/// Represents the <see cref="InvitationCancelledIntegrationEvent"/> handler.
/// </summary>
internal sealed class InvitationCancelledIntegrationEventHandler : IntegrationEventHandler<InvitationCancelledIntegrationEvent>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InvitationCancelledIntegrationEventHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvitationCancelledIntegrationEventHandler"/> class.
    /// </summary>
    /// <param name="userRegistrationRepository">The user registration repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    public InvitationCancelledIntegrationEventHandler(
        IUserRegistrationRepository userRegistrationRepository,
        IUnitOfWork unitOfWork,
        ILogger<InvitationCancelledIntegrationEventHandler> logger)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task Handle(InvitationCancelledIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await GetUserRegistrationByIdAsync(new UserRegistrationId(integrationEvent.InvitationId), cancellationToken)
            .Bind(userRegistration => userRegistration.Cancel())
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .OnFailure(error => _logger.LogError(integrationEvent, error));

    private async Task<Result<UserRegistration>> GetUserRegistrationByIdAsync(
        UserRegistrationId userRegistrationId,
        CancellationToken cancellationToken) =>
        await _userRegistrationRepository.GetByIdAsync(userRegistrationId, cancellationToken)
            .MapFailure(() => UserRegistrationErrors.NotFound(userRegistrationId));
}
