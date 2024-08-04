namespace Modules.Users.Endpoints.Contracts.Users;

/// <summary>
/// Represents the request for registering a new user.
/// </summary>
/// <param name="Email">The email.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
public sealed record RegisterUserRequest(string Email, string FirstName, string LastName);
