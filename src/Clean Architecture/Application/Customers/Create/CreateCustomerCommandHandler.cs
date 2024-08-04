using Domain.Customers;
using MediatR;

namespace Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(new CustomerId(Guid.NewGuid()), request.Email, request.Email);

        _customerRepository.Add(customer);

        return Task.CompletedTask;
    }
}
