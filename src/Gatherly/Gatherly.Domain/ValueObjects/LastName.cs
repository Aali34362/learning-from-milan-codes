using Gatherly.Domain.Errors;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public sealed class LastName
{
    public const int MaxLength = 50;

    private LastName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static LastName Create(string lastName)
    {
        Ensure.NotNullOrWhiteSpace(lastName, DomainErrors.LastName.Empty);
        Ensure.NotGreaterThan(lastName.Length, MaxLength, DomainErrors.LastName.TooLong);

        return new LastName(lastName);
    }
}
