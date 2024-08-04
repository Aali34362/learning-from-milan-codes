using Gatherly.Domain.Errors;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public sealed record FirstName
{
    public const int MaxLength = 50;

    private FirstName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static FirstName Create(string firstName)
    {
        Ensure.NotNullOrWhiteSpace(firstName, DomainErrors.FirstName.Empty);
        Ensure.NotGreaterThan(firstName.Length, MaxLength, DomainErrors.FirstName.TooLong);

        return new FirstName(firstName);
    }
}
