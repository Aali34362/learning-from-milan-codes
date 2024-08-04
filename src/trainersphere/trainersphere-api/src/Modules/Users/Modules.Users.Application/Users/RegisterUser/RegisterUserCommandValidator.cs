using Application.Extensions;
using FluentValidation;
using Modules.Users.Application.ValidationErrors;

namespace Modules.Users.Application.Users.RegisterUser;

/// <summary>
/// Represents the <see cref="RegisterUserCommand"/> validator.
/// </summary>
internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserCommandValidator"/> class.
    /// </summary>
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.IdentityProviderId).NotEmpty().WithError(UserValidationErrors.IdentityIsRequired);

        RuleFor(x => x.Email).NotEmpty().WithError(UserValidationErrors.EmailIsRequired);

        RuleFor(x => x.FirstName).NotEmpty().WithError(UserValidationErrors.FirstNameIsRequired);

        RuleFor(x => x.LastName).NotEmpty().WithError(UserValidationErrors.LastNameIsRequired);
    }
}
