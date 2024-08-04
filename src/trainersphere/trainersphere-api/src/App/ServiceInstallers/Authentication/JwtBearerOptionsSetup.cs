using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace App.ServiceInstallers.Authentication;

/// <summary>
/// Represents the <see cref="JwtBearerOptions"/> setup.
/// </summary>
internal sealed class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private const string ConfigurationSectionName = "JwtBearer";
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtBearerOptionsSetup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public JwtBearerOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    /// <inheritdoc />
    public void Configure(JwtBearerOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);

    public void Configure(string name, JwtBearerOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}
