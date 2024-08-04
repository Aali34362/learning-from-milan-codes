namespace Infrastructure.Outbox;

internal sealed record OutboxMessage(
    Guid Id,
    string Name,
    string Content,
    DateTime CreatedOnUtc,
    DateTime? ProcessedOnUtc = null,
    string? Error = null);
