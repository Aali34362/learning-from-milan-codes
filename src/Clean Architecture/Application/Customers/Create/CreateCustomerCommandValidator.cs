using Domain.Customers;
using FluentValidation;

namespace Application.Customers.Create;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        RuleFor(c => c.Email).EmailAddress();

        RuleFor(c => c.Email)
            .MustAsync(async (email, _) => await customerRepository.IsEmailUniqueAsync(email))
            .WithMessage("The email must be unique");
    }
}
