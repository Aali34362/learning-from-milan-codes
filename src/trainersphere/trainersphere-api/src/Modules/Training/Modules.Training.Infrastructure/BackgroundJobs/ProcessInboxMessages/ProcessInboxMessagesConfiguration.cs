using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;

namespace Modules.Training.Infrastructure.BackgroundJobs.ProcessInboxMessages;

/// <summary>
/// Represents the <see cref="ProcessInboxMessagesJob"/> configuration.
/// </summary>
internal sealed class ProcessInboxMessagesConfiguration : IRecurringJobConfiguration
{
    private readonly ProcessInboxMessagesOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessInboxMessagesConfiguration"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public ProcessInboxMessagesConfiguration(IOptions<ProcessInboxMessagesOptions> options) => _options = options.Value;

    /// <inheritdoc />
    public string Name => typeof(ProcessInboxMessagesJob).FullName!;

    /// <inheritdoc />
    public Type Type => typeof(ProcessInboxMessagesJob);

    /// <inheritdoc />
    public int IntervalInSeconds => _options.IntervalInSeconds;
}
