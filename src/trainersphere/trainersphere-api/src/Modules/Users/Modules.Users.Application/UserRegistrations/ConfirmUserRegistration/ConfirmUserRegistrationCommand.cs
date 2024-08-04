using Application.Messaging;

namespace Modules.Users.Application.UserRegistrations.ConfirmUserRegistration;

/// <summary>
/// Represents the command for confirming a user registration.
/// </summary>
/// <param name="UserRegistrationId">The user registration identifier.</param>
/// <param name="IdentityProviderId">The identity provider identifier.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
public sealed record ConfirmUserRegistrationCommand(
    Guid UserRegistrationId,
    string IdentityProviderId,
    string Email,
    string FirstName,
    string LastName) : ICommand;
