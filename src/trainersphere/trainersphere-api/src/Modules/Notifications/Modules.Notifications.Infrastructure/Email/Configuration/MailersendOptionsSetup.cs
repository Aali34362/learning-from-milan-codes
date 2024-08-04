using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Modules.Notifications.Infrastructure.Email.Configuration;

/// <summary>
/// Represents the <see cref="MailersendOptions"/> setup.
/// </summary>
internal sealed class MailersendOptionsSetup : IConfigureOptions<MailersendOptions>
{
    private const string ConfigurationSectionName = "Modules:Notifications:Mailersend";
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="MailersendOptionsSetup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public MailersendOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    /// <inheritdoc />
    public void Configure(MailersendOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}
