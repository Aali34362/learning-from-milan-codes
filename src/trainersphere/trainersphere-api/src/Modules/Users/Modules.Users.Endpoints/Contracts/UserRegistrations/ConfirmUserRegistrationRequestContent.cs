namespace Modules.Users.Endpoints.Contracts.UserRegistrations;

/// <summary>
/// Represents the confirm user registration request content.
/// </summary>
/// <param name="Email">The email.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
public sealed record ConfirmUserRegistrationRequestContent(string Email, string FirstName, string LastName);
