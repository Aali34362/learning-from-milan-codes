namespace Persistence.Outbox;

/// <summary>
/// Represents the outbox message consumer.
/// </summary>
public sealed class OutboxMessageConsumer
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;
}
