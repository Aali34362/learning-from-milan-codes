using Microsoft.AspNetCore.Mvc;
using Modules.Users.Endpoints.Routes;

namespace Modules.Users.Endpoints.Contracts.UserRegistrations;

/// <summary>
/// Represents the request for updating a user's basic information.
/// </summary>
public sealed class ConfirmUserRegistrationRequest
{
    /// <summary>
    /// Gets the user registration identifier.
    /// </summary>
    [FromRoute(Name = UserRegistrationsRoutes.ResourceId)]
    public Guid UserRegistrationId { get; init; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    [FromBody]
    public ConfirmUserRegistrationRequestContent Content { get; init; } = new(string.Empty, string.Empty, string.Empty);
}
