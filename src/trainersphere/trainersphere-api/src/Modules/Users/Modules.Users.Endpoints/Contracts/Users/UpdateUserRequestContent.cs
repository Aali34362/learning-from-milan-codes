namespace Modules.Users.Endpoints.Contracts.Users;

/// <summary>
/// Represents the update user request content.
/// </summary>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
public sealed record UpdateUserRequestContent(string FirstName, string LastName);
