using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Infrastructure.Configuration;
using Shared.Extensions;

namespace App.ServiceInstallers.Authentication;

/// <summary>
/// Represents the authentication service installer.
/// </summary>
internal sealed class AuthenticationServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .ConfigureOptions<AuthenticationOptionsSetup>()
            .ConfigureOptions<JwtBearerOptionsSetup>()
            .AddAuthentication()
            .AddJwtBearer()
            .Tap(ReplaceSubClaimWithName);

    private static void ReplaceSubClaimWithName() =>
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = ClaimTypes.NameIdentifier;
}
