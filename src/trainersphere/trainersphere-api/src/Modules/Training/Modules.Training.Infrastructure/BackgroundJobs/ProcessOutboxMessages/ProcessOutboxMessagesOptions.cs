namespace Modules.Training.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

/// <summary>
/// Represents the <see cref="ProcessOutboxMessagesJob"/> options.
/// </summary>
internal sealed class ProcessOutboxMessagesOptions
{
    /// <summary>
    /// Gets the interval in seconds.
    /// </summary>
    public int IntervalInSeconds { get; init; }

    /// <summary>
    /// Gets the retry count.
    /// </summary>
    public int RetryCount { get; init; }

    /// <summary>
    /// Gets the batch size.
    /// </summary>
    public int BatchSize { get; init; }
}
