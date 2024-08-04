using System.Security.Claims;

namespace Endpoints.Extensions;

/// <summary>
/// Contains extension methods for the <see cref="ClaimsPrincipal"/> class.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Gets the identity provider identifier of the currently authenticated user.
    /// </summary>
    /// <param name="claimsPrincipal">The claims principal.</param>
    /// <returns>The identity provider identifier of the currently authenticated user if it exists, or an empty string.</returns>
    public static string GetIdentityProviderId(this ClaimsPrincipal claimsPrincipal) =>
        claimsPrincipal.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
}
