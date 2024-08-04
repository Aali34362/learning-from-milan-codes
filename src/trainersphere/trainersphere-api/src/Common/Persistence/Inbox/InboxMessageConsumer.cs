namespace Persistence.Inbox;

/// <summary>
/// Represents the inbox message consumer.
/// </summary>
public sealed class InboxMessageConsumer
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
