using Domain.Primitives;
using Domain.Time;
using Modules.Users.Domain.UserRegistrations.Events;
using Shared.Results;

namespace Modules.Users.Domain.UserRegistrations;

/// <summary>
/// Represents the user registration entity.
/// </summary>
public sealed class UserRegistration : Entity<UserRegistrationId>, IAuditable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegistration"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="status">The status.</param>
    private UserRegistration(UserRegistrationId id, string email, UserRegistrationStatus status)
        : base(id)
    {
        Email = email;
        Status = status;
    }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the identity provider identifier.
    /// </summary>
    public string? IdentityProviderId { get; private set; }

    /// <summary>
    /// Gets the first name.
    /// </summary>
    public string? FirstName { get; private set; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public string? LastName { get; private set; }

    /// <summary>
    /// Gets the status.
    /// </summary>
    public UserRegistrationStatus Status { get; private set; }

    /// <summary>
    /// Gets the confirmed on date and time.
    /// </summary>
    public DateTime? ConfirmedOnUtc { get; private set; }

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; private set; }

    /// <summary>
    /// Creates a new user registration with the specified parameters.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="email">The email.</param>
    /// <returns>The new user registration instance.</returns>
    public static Result<UserRegistration> Create(UserRegistrationId id, string email)
    {
        var userRegistration = new UserRegistration(id, email, UserRegistrationStatus.Pending);

        return userRegistration;
    }

    /// <summary>
    /// Confirms the user registration with the specified parameters.
    /// </summary>
    /// <param name="identityProviderId">The identity provider identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>The result of the confirmation operation.</returns>
    public Result Confirm(string identityProviderId, string email, string firstName, string lastName)
    {
        if (Email != email)
        {
            return Result.Failure(UserRegistrationErrors.EmailDoesNotMatch);
        }

        return Status switch
        {
            var status when status == UserRegistrationStatus.Confirmed => Result.Failure(UserRegistrationErrors.Confirmed(Id)),
            var status when status == UserRegistrationStatus.Cancelled => Result.Failure(UserRegistrationErrors.Cancelled(Id)),
            var status when status == UserRegistrationStatus.Expired => Result.Failure(UserRegistrationErrors.Expired(Id)),
            _ => ConfirmInternal(identityProviderId, firstName, lastName)
        };
    }

    /// <summary>
    /// Cancels the user registration.
    /// </summary>
    /// <returns>The result of the cancellation operation.</returns>
    public Result Cancel() =>
        Status switch
        {
            var status when status == UserRegistrationStatus.Cancelled => Result.Failure(UserRegistrationErrors.Cancelled(Id)),
            _ => CancelInternal()
        };

    /// <summary>
    /// Expires the user registration.
    /// </summary>
    /// <returns>The result of the expiration operation.</returns>
    public Result Expire() =>
        Status switch
        {
            var status when status == UserRegistrationStatus.Expired => Result.Failure(UserRegistrationErrors.Expired(Id)),
            _ => ExpireInternal()
        };

    private Result ConfirmInternal(string identityProviderId, string firstName, string lastName)
    {
        IdentityProviderId = identityProviderId;
        FirstName = firstName;
        LastName = lastName;
        Status = UserRegistrationStatus.Confirmed;
        ConfirmedOnUtc = SystemTimeProvider.UtcNow();

        RaiseDomainEvent(new UserRegistrationConfirmedDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            Id));

        return Result.Success();
    }

    private Result CancelInternal()
    {
        Status = UserRegistrationStatus.Cancelled;

        return Result.Success();
    }

    private Result ExpireInternal()
    {
        Status = UserRegistrationStatus.Expired;

        RaiseDomainEvent(new UserRegistrationExpiredDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            Id));

        return Result.Success();
    }
}
