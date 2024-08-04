using Application.Extensions;
using FluentValidation;
using Modules.Users.Application.ValidationErrors;

namespace Modules.Users.Application.Users.UpdateUser;

/// <summary>
/// Represents the <see cref="UpdateUserCommand"/> validator.
/// </summary>
internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserCommandValidator"/> class.
    /// </summary>
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithError(UserValidationErrors.IdentifierIsRequired);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithError(UserValidationErrors.FirstNameIsRequired);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithError(UserValidationErrors.LastNameIsRequired);
    }
}
