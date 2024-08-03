using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Outbox;

public sealed class OutboxSettings
{
    [Required]
    public int IntervalInSeconds { get; set; }

    [Range(1, 100)]
    public int BatchSize { get; set; }

    [Range(1, 5)]
    public int RetryThreshold { get; set; }
}
