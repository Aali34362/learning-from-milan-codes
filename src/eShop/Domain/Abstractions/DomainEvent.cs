namespace Domain.Abstractions;

/// <summary>
/// Represents the abstract domain event primitive.
/// </summary>
public abstract record DomainEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEvent"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="occurredOnUtc">The occurred on date and time.</param>
    protected DomainEvent(Guid id, DateTime occurredOnUtc)
        : this()
    {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEvent"/> class.
    /// </summary>
    private DomainEvent()
    {
    }

    public Guid Id { get; init; }

    public DateTime OccurredOnUtc { get; init; }
}
