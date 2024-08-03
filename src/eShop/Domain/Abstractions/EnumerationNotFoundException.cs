namespace Domain.Abstractions;

public sealed class EnumerationNotFoundException : ArgumentOutOfRangeException
{
    public EnumerationNotFoundException(string name, int value)
        : base($"The {name} with the value {value} was not found.")
    {
    }
}
