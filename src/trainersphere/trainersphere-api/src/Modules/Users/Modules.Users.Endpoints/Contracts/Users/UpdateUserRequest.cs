using Microsoft.AspNetCore.Mvc;
using Modules.Users.Endpoints.Routes;

namespace Modules.Users.Endpoints.Contracts.Users;

/// <summary>
/// Represents the request for updating a user's basic information.
/// </summary>
public sealed class UpdateUserRequest
{
    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    [FromRoute(Name = UsersRoutes.ResourceId)]
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    [FromBody]
    public UpdateUserRequestContent Content { get; init; } = new(string.Empty, string.Empty);
}
