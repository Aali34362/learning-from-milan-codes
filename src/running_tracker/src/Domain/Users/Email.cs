using SharedKernel;

namespace Domain.Users;

public sealed record Email
{
    private Email(string value) => Value = value;

    public string Value { get; }

    public static Result<Email> Create(string? email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return Result.Failure<Email>(EmailErrors.Empty);
        }

        if (email.Split('@').Length != 2)
        {
            return Result.Failure<Email>(EmailErrors.InvalidFormat);
        }

        return new Email(email);
    }
}

public static class EmailErrors
{
    public static readonly Error Empty = new("Email.Empty", "Email is empty");

    public static readonly Error InvalidFormat = new(
        "Email.InvalidFormat", "Email format is invalid");
}
