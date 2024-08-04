using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Modules.Users.Endpoints;
using Modules.Users.Infrastructure.Authorization.SameUser.Requirements;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the <see cref="AuthorizationOptions"/> setup.
/// </summary>
internal sealed class AuthorizationOptionsSetup : IConfigureOptions<AuthorizationOptions>
{
    /// <inheritdoc />
    public void Configure(AuthorizationOptions options) =>
        options.AddPolicy(
            Policies.SameUser,
            policy => policy.AddRequirements(new SameUserRequirement()));
}
