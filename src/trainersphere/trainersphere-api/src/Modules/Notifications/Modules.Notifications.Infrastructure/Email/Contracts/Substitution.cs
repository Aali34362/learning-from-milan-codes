namespace Modules.Notifications.Infrastructure.Email.Contracts;

/// <summary>
/// Represents the substitution.
/// </summary>
/// <param name="Var">The variable.</param>
/// <param name="Value">The value.</param>
internal sealed record Substitution(string Var, string Value);
