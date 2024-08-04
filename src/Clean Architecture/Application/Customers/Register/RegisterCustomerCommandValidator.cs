using Domain.Customers;
using FluentValidation;

namespace Application.Customers.Register;

public sealed class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        RuleFor(c => c.Email).MustAsync(async (email, _) =>
        {
            return await customerRepository.IsEmailUniqueAsync(email);
        }).WithMessage("The email must be unique");
    }
}
