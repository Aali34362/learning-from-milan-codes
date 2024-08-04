using Shared.Errors;

namespace Modules.Users.Application.ValidationErrors;

/// <summary>
/// Contains the user registration validation errors.
/// </summary>
internal static class UserRegistrationValidationErrors
{
    /// <summary>
    /// Gets the identifier is required error.
    /// </summary>
    internal static Error IdentifierIsRequired => new("UserRegistration.IdentifierIsRequired", "The user identifier is required.");

    /// <summary>
    /// Gets the identity is required error.
    /// </summary>
    internal static Error IdentityIsRequired => new("UserRegistration.IdentityIsRequired", "The user registration identity is required.");

    /// <summary>
    /// Gets the email is required error.
    /// </summary>
    internal static Error EmailIsRequired => new("UserRegistration.EmailIsRequired", "The user registration email is required.");

    /// <summary>
    /// Gets the first name is required error.
    /// </summary>
    internal static Error FirstNameIsRequired => new("UserRegistration.FirstNameIsRequired", "The user registration first name is required.");

    /// <summary>
    /// Gets the last name is required error.
    /// </summary>
    internal static Error LastNameIsRequired => new("UserRegistration.LastNameIsRequired", "The user registration last name is required.");
}
