using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Authorization.Options;

/// <summary>
/// Represents the <see cref="PermissionAuthorizationOptions"/> setup.
/// </summary>
internal sealed class PermissionAuthorizationOptionsSetup : IConfigureOptions<PermissionAuthorizationOptions>
{
    private const string ConfigurationSectionName = "Authorization:Permissions";
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionAuthorizationOptionsSetup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public PermissionAuthorizationOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    /// <inheritdoc />
    public void Configure(PermissionAuthorizationOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}
