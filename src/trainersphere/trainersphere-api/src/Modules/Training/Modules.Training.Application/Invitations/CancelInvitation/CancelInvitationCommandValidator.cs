using Application.Extensions;
using FluentValidation;
using Modules.Training.Application.ValidationErrors;

namespace Modules.Training.Application.Invitations.CancelInvitation;

/// <summary>
/// Represents the <see cref="CancelInvitationCommand"/> class.
/// </summary>
internal sealed class CancelInvitationCommandValidator : AbstractValidator<CancelInvitationCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CancelInvitationCommandValidator"/> class.
    /// </summary>
    public CancelInvitationCommandValidator() => RuleFor(x => x.InvitationId).NotEmpty().WithError(InvitationValidationErrors.IdentifierIsRequired);
}
