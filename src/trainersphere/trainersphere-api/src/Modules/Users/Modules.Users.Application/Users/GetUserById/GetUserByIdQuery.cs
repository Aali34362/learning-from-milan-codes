using Application.Messaging;

namespace Modules.Users.Application.Users.GetUserById;

/// <summary>
/// Represents the query for getting a user by identifier.
/// </summary>
/// <param name="UserId">The user identifier.</param>
public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
