using Shared.Errors;

namespace Modules.Users.Domain.Users;

/// <summary>
/// Contains the users errors.
/// </summary>
public static class UserErrors
{
    /// <summary>
    /// Gets the email is not unique error.
    /// </summary>
    public static Error EmailIsNotUnique => new ConflictError("User.EmailIsNotUnique", "The specified email is already in use.");

    /// <summary>
    /// Gets the registration is not confirmed error.
    /// </summary>
    public static Error RegistrationIsNotConfirmed => new("User.RegistrationIsNotConfirmed", "The user registration is not confirmed.");

    /// <summary>
    /// Gets the registration incomplete error.
    /// </summary>
    public static Error RegistrationIsIncomplete => new("User.RegistrationIsIncomplete", "The user registration is incomplete.");

    /// <summary>
    /// Gets the not found error.
    /// </summary>
    public static Func<UserId, Error> NotFound => userId => new NotFoundError(
        "User.NotFound",
        $"The user with the identifier '{userId.Value}' was not found.");

    /// <summary>
    /// Gets the not found by identity error.
    /// </summary>
    public static Func<string, Error> NotFoundByIdentity =>
        identityProviderId => new NotFoundError(
            "User.NotFoundByIdentity",
            $"The user with the identity provider identifier '{identityProviderId}' was not found.");
}
