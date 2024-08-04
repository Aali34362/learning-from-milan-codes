using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Modules.Users.Infrastructure.Authorization.SameUser.Options;

/// <summary>
/// Represents the <see cref="SameUserAuthorizationOptions"/> setup.
/// </summary>
internal sealed class SameUserAuthorizationOptionsSetup : IConfigureOptions<SameUserAuthorizationOptions>
{
    private const string ConfigurationSectionName = "Modules:Users:Authorization:SameUser";
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="SameUserAuthorizationOptionsSetup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public SameUserAuthorizationOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    /// <inheritdoc />
    public void Configure(SameUserAuthorizationOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}
