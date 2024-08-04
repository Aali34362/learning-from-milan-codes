namespace Modules.Notifications.Infrastructure.BackgroundJobs.ProcessInboxMessages;

/// <summary>
/// Represents the <see cref="ProcessInboxMessagesJob"/> options.
/// </summary>
internal sealed class ProcessInboxMessagesOptions
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
