using Shared.Extensions;

namespace Modules.Notifications.Infrastructure.Email.Contracts;

/// <summary>
/// Represents the variable.
/// </summary>
internal sealed record Variable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Variable"/> class.
    /// </summary>
    private Variable()
    {
    }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Gets the substitutions.
    /// </summary>
    public Substitution[] Substitutions { get; init; } = Array.Empty<Substitution>();

    /// <summary>
    /// Creates a new variable with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>The newly created variable instance.</returns>
    public static Variable Create(string email) => new()
    {
        Email = email
    };

    /// <summary>
    /// Creates a new variable instance from the existing instance with the specified var and value.
    /// </summary>
    /// <param name="var">The variable name.</param>
    /// <param name="value">The variable value.</param>
    /// <returns>The newly created variable instance.</returns>
    public Variable WithSubstitution(string var, string value) => this with
    {
        Substitutions = new List<Substitution> { new(var, value) }
            .Tap(list => list.AddRange(Substitutions))
            .ToArray()
    };
}
