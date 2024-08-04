using Authorization.AuthorizationHandlers;
using Authorization.AuthorizationPolicyProviders;
using Authorization.Options;
using Authorization.Services;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization;

/// <summary>
/// Represents the authorization service installer.
/// </summary>
internal sealed class AuthorizationServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddAuthorization()
            .ConfigureOptions<PermissionAuthorizationOptionsSetup>()
            .AddScoped<IPermissionService, PermissionService>()
            .AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>()
            .AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
}
