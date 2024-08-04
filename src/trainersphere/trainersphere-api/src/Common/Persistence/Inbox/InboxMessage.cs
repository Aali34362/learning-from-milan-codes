namespace Persistence.Inbox;

/// <summary>
/// Represents the inbox message.
/// </summary>
public sealed class InboxMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InboxMessage"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="occurredOnUtc">The occurred on date and time.</param>
    /// <param name="type">The type.</param>
    /// <param name="content">The content.</param>
    public InboxMessage(Guid id, DateTime occurredOnUtc, string type, string content)
    {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
        Content = content;
        Type = type;
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the occurred on date and time.
    /// </summary>
    public DateTime OccurredOnUtc { get; private set; }

    /// <summary>
    /// Gets the type.
    /// </summary>
    public string Type { get; private set; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Gets the processed on date and time, if it exists.
    /// </summary>
    public DateTime? ProcessedOnUtc { get; private set; }

    /// <summary>
    /// Gets the error, if it exists.
    /// </summary>
    public string? Error { get; private set; }
}
