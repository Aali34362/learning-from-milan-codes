namespace Gatherly.Domain.Entities;

public sealed class MemberSnapshot
{
    public Guid Id { get; init; }

    public string Email { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public DateTime CreatedOnUtc { get; init; }

    public DateTime? ModifiedOnUtc { get; init; }
}
