using Shared.Errors;

namespace Modules.Users.Application.ValidationErrors;

/// <summary>
/// Contains the user validation errors.
/// </summary>
internal static class UserValidationErrors
{
    /// <summary>
    /// Gets the identifier is required error.
    /// </summary>
    internal static Error IdentifierIsRequired => new("User.IdentifierIsRequired", "The user identifier is required.");

    /// <summary>
    /// Gets the identity is required error.
    /// </summary>
    internal static Error IdentityIsRequired => new("User.IdentityIsRequired", "The user's identity is required.");

    /// <summary>
    /// Gets the email is required error.
    /// </summary>
    internal static Error EmailIsRequired => new("User.EmailIsRequired", "The user's email is required.");

    /// <summary>
    /// Gets the first name is required error.
    /// </summary>
    internal static Error FirstNameIsRequired => new("User.FirstNameIsRequired", "The user's first name is required.");

    /// <summary>
    /// Gets the last name is required error.
    /// </summary>
    internal static Error LastNameIsRequired => new("User.LastNameIsRequired", "The user's last name is required.");
}
