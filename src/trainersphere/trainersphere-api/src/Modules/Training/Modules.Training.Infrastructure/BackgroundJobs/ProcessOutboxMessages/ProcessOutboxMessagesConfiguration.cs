using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;

namespace Modules.Training.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

/// <summary>
/// Represents the <see cref="ProcessOutboxMessagesJob"/> configuration.
/// </summary>
internal sealed class ProcessOutboxMessagesConfiguration : IRecurringJobConfiguration
{
    private readonly ProcessOutboxMessagesOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessOutboxMessagesConfiguration"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public ProcessOutboxMessagesConfiguration(IOptions<ProcessOutboxMessagesOptions> options) => _options = options.Value;

    /// <inheritdoc />
    public string Name => typeof(ProcessOutboxMessagesJob).FullName!;

    /// <inheritdoc />
    public Type Type => typeof(ProcessOutboxMessagesJob);

    /// <inheritdoc />
    public int IntervalInSeconds => _options.IntervalInSeconds;
}
