using Microsoft.AspNetCore.Authorization;

namespace Modules.Users.Infrastructure.Authorization.SameUser.Requirements;

/// <summary>
/// Represents the same user authorization requirement.
/// </summary>
internal sealed class SameUserRequirement : IAuthorizationRequirement
{
}
