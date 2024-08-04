using Application.Extensions;
using Application.Messaging;
using Microsoft.Extensions.Logging;
using Modules.Users.Domain;
using Modules.Users.Domain.UserRegistrations;
using Modules.Users.Domain.UserRegistrations.Events;
using Modules.Users.Domain.Users;
using Shared.Results;

namespace Modules.Users.Application.UserRegistrations.ConfirmUserRegistration;

/// <summary>
/// Represents the <see cref="UserRegistrationConfirmedDomainEvent"/> handler.
/// </summary>
internal sealed class UserRegistrationConfirmedDomainEventHandler : IDomainEventHandler<UserRegistrationConfirmedDomainEvent>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserRegistrationConfirmedDomainEventHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegistrationConfirmedDomainEventHandler"/> class.
    /// </summary>
    /// <param name="userRegistrationRepository">The user registration repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    public UserRegistrationConfirmedDomainEventHandler(
        IUserRegistrationRepository userRegistrationRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserRegistrationConfirmedDomainEventHandler> logger)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task Handle(UserRegistrationConfirmedDomainEvent notification, CancellationToken cancellationToken) =>
        await GetUserRegistrationByIdAsync(notification.UserRegistrationId, cancellationToken)
            .Bind(User.CreateFromRegistration)
            .Tap(user => _userRepository.Add(user))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .OnFailure(error => _logger.LogError(notification, error));

    private async Task<Result<UserRegistration>> GetUserRegistrationByIdAsync(
        UserRegistrationId userRegistrationId,
        CancellationToken cancellationToken) =>
        await _userRegistrationRepository.GetByIdAsync(userRegistrationId, cancellationToken)
            .MapFailure(() => UserRegistrationErrors.NotFound(userRegistrationId));
}
