using Domain.Primitives;
using Domain.Time;
using Modules.Users.Domain.Roles;
using Modules.Users.Domain.UserRegistrations;
using Modules.Users.Domain.Users.Events;
using Shared.Results;

namespace Modules.Users.Domain.Users;

/// <summary>
/// Represents the user entity.
/// </summary>
public sealed class User : Entity<UserId>, IAuditable
{
    private readonly HashSet<Role> _roles = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="identityProviderId">The identity provider identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    private User(UserId id, string identityProviderId, string email, string firstName, string lastName)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        IdentityProviderId = identityProviderId;
    }

    /// <summary>
    /// Gets the identity provider identifier.
    /// </summary>
    public string IdentityProviderId { get; private set; }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the first name.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Gets the roles.
    /// </summary>
    public IReadOnlyCollection<Role> Roles => _roles.ToList().AsReadOnly();

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; private set; }

    /// <summary>
    /// Creates a new user with the specified parameters.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="identityProviderId">The identity provider identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>The new user instance.</returns>
    public static Result<User> Create(UserId id, string identityProviderId, string email, string firstName, string lastName)
    {
        var user = new User(id, identityProviderId, email, firstName, lastName);

        user._roles.Add(Role.Registered);
        user._roles.Add(Role.Trainer);

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            user.Id,
            null,
            user.Email,
            user.FirstName,
            user.LastName,
            user._roles.Select(role => role.Name).ToHashSet()));

        return user;
    }

    /// <summary>
    /// Creates a new user based on the specified user registration.
    /// </summary>
    /// <param name="userRegistration">The user registration.</param>
    /// <returns>The new user instance.</returns>
    public static Result<User> CreateFromRegistration(UserRegistration userRegistration)
    {
        if (userRegistration.Status != UserRegistrationStatus.Confirmed)
        {
            return Result.Failure<User>(UserErrors.RegistrationIsNotConfirmed);
        }

        if (string.IsNullOrWhiteSpace(userRegistration.IdentityProviderId) ||
            string.IsNullOrWhiteSpace(userRegistration.FirstName) ||
            string.IsNullOrWhiteSpace(userRegistration.LastName))
        {
            return Result.Failure<User>(UserErrors.RegistrationIsIncomplete);
        }

        var user = new User(
            new UserId(Guid.NewGuid()),
            userRegistration.IdentityProviderId,
            userRegistration.Email,
            userRegistration.FirstName,
            userRegistration.LastName);

        user._roles.Add(Role.Registered);
        user._roles.Add(Role.Client);

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            user.Id,
            userRegistration.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user._roles.Select(role => role.Name).ToHashSet()));

        return user;
    }

    /// <summary>
    /// Changes the user's basic information.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    public void Change(string firstName, string lastName)
    {
        bool basicInformationChanged = FirstName != firstName || LastName != lastName;

        FirstName = firstName;
        LastName = lastName;

        if (basicInformationChanged)
        {
            RaiseDomainEvent(new UserChangedDomainEvent(
                Guid.NewGuid(),
                SystemTimeProvider.UtcNow(),
                Id,
                FirstName,
                LastName,
                _roles.Select(role => role.Name).ToHashSet()));
        }
    }
}
