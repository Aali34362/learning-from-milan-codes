using Application.Extensions;
using FluentValidation;
using Modules.Training.Application.ValidationErrors;

namespace Modules.Training.Application.Trainers.InviteClient;

/// <summary>
/// Represents the <see cref="InviteClientCommand"/> validator.
/// </summary>
internal sealed class InviteClientCommandValidator : AbstractValidator<InviteClientCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InviteClientCommandValidator"/> class.
    /// </summary>
    public InviteClientCommandValidator()
    {
        RuleFor(x => x.TrainerId).NotEmpty().WithError(InvitationValidationErrors.TrainerIdentifierIsRequired);

        RuleFor(x => x.Email).NotEmpty().WithError(InvitationValidationErrors.EmailIsRequired);
    }
}
