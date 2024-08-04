using Application.Messaging;
using Modules.Users.Domain;
using Modules.Users.Domain.UserRegistrations;
using Modules.Users.Domain.Users;
using Shared.Results;

namespace Modules.Users.Application.UserRegistrations.ConfirmUserRegistration;

/// <summary>
/// Represents the <see cref="ConfirmUserRegistrationCommand"/> handler.
/// </summary>
internal sealed class ConfirmUserRegistrationCommandHandler : ICommandHandler<ConfirmUserRegistrationCommand>
{
    private readonly IIdentityProviderService _identityProviderService;
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmUserRegistrationCommandHandler"/> class.
    /// </summary>
    /// <param name="identityProviderService">The identity provider service.</param>
    /// <param name="userRegistrationRepository">The user registration repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public ConfirmUserRegistrationCommandHandler(
        IIdentityProviderService identityProviderService,
        IUserRegistrationRepository userRegistrationRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _identityProviderService = identityProviderService;
        _userRegistrationRepository = userRegistrationRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken) =>
        await GetUserRegistrationByIdAsync(new UserRegistrationId(request.UserRegistrationId), cancellationToken)
            .Bind(userRegistration => CheckIfIdentityExistsAsync(userRegistration, request.IdentityProviderId, cancellationToken))
            .Bind(userRegistration => CheckIfEmailIsUniqueAsync(userRegistration, cancellationToken))
            .Bind(userRegistration => userRegistration.Confirm(
                request.IdentityProviderId,
                request.Email,
                request.FirstName,
                request.LastName))
            .Tap(() => _unitOfWork.SaveChangesAsync(cancellationToken));

    private async Task<Result<UserRegistration>> GetUserRegistrationByIdAsync(
        UserRegistrationId userRegistrationId,
        CancellationToken cancellationToken) =>
        await _userRegistrationRepository.GetByIdAsync(userRegistrationId, cancellationToken)
            .MapFailure(() => UserRegistrationErrors.NotFound(userRegistrationId));

    private async Task<Result<UserRegistration>> CheckIfIdentityExistsAsync(
        UserRegistration userRegistration,
        string identityProviderId,
        CancellationToken cancellationToken) =>
        await _identityProviderService.ExistsAsync(identityProviderId, cancellationToken)
            .Map(() => userRegistration)
            .MapFailure(() => UserRegistrationErrors.NotFoundByIdentity(identityProviderId));

    private async Task<Result<UserRegistration>> CheckIfEmailIsUniqueAsync(
        UserRegistration userRegistration,
        CancellationToken cancellationToken) =>
        await _userRepository.IsEmailUniqueAsync(userRegistration.Email, cancellationToken)
            .Map(() => userRegistration)
            .MapFailure(() => UserRegistrationErrors.EmailHasBeenTaken);
}
