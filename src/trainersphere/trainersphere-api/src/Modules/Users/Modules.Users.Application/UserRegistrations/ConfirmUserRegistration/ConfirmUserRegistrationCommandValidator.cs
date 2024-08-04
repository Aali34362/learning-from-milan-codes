using Application.Extensions;
using FluentValidation;
using Modules.Users.Application.ValidationErrors;

namespace Modules.Users.Application.UserRegistrations.ConfirmUserRegistration;

/// <summary>
/// Represents the <see cref="ConfirmUserRegistrationCommand"/> validator.
/// </summary>
internal sealed class ConfirmUserRegistrationCommandValidator : AbstractValidator<ConfirmUserRegistrationCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmUserRegistrationCommandValidator"/> class.
    /// </summary>
    public ConfirmUserRegistrationCommandValidator()
    {
        RuleFor(x => x.UserRegistrationId).NotEmpty().WithError(UserRegistrationValidationErrors.IdentifierIsRequired);

        RuleFor(x => x.IdentityProviderId).NotEmpty().WithError(UserRegistrationValidationErrors.IdentityIsRequired);

        RuleFor(x => x.Email).NotEmpty().WithError(UserRegistrationValidationErrors.EmailIsRequired);

        RuleFor(x => x.FirstName).NotEmpty().WithError(UserRegistrationValidationErrors.FirstNameIsRequired);

        RuleFor(x => x.LastName).NotEmpty().WithError(UserRegistrationValidationErrors.LastNameIsRequired);
    }
}
