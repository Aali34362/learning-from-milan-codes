using Application.Messaging;

namespace Modules.Users.Application.Users.RegisterUser;

/// <summary>
/// Represents the command for registering a new user.
/// </summary>
/// <param name="IdentityProviderId">The identity provider identifier.</param>
/// <param name="Email">The email.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
public sealed record RegisterUserCommand(string IdentityProviderId, string Email, string FirstName, string LastName) : ICommand<Guid>;
