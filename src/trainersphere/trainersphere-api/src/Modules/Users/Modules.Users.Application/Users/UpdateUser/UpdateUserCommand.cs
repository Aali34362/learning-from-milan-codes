using Application.Messaging;

namespace Modules.Users.Application.Users.UpdateUser;

/// <summary>
/// Represents the command for updating a user's basic information.
/// </summary>
/// <param name="UserId">The user identifier.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
