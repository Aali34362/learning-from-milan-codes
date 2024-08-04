using Application.EventBus;
using Application.Extensions;
using Microsoft.Extensions.Logging;
using Modules.Training.Domain;
using Modules.Training.Domain.Clients;
using Modules.Users.IntegrationEvents;
using Shared.Results;

namespace Modules.Training.Application.Clients.Events;

/// <summary>
/// Represents the <see cref="UserChangedIntegrationEvent"/> handler.
/// </summary>
internal sealed class UserChangedIntegrationEventHandler : IntegrationEventHandler<UserChangedIntegrationEvent>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserChangedIntegrationEvent> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserChangedIntegrationEventHandler"/> class.
    /// </summary>
    /// <param name="clientRepository">The trainer repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    public UserChangedIntegrationEventHandler(
        IClientRepository clientRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserChangedIntegrationEvent> logger)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task Handle(UserChangedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        await Result.Create(integrationEvent)
            .Filter(userRegisteredIntegrationEvent => userRegisteredIntegrationEvent.Roles.Contains(Client.Role))
            .Bind(() => GetClientByIdAsync(new ClientId(integrationEvent.UserId), cancellationToken))
            .Tap(client => client.Change(integrationEvent.FirstName, integrationEvent.LastName))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .OnFailure(error => _logger.LogError(integrationEvent, error));

    private async Task<Result<Client>> GetClientByIdAsync(ClientId clientId, CancellationToken cancellationToken) =>
        await _clientRepository.GetByIdAsync(clientId, cancellationToken)
            .MapFailure(() => ClientErrors.NotFound(clientId));
}
