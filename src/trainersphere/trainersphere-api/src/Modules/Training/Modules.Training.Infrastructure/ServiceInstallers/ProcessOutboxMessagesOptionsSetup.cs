using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Modules.Training.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

namespace Modules.Training.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the <see cref="ProcessOutboxMessagesOptions"/> setup.
/// </summary>
internal sealed class ProcessOutboxMessagesOptionsSetup : IConfigureOptions<ProcessOutboxMessagesOptions>
{
    private const string ConfigurationSectionName = "Modules:Training:BackgroundJobs:ProcessOutboxMessages";
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessOutboxMessagesOptionsSetup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public ProcessOutboxMessagesOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    /// <inheritdoc />
    public void Configure(ProcessOutboxMessagesOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
}
