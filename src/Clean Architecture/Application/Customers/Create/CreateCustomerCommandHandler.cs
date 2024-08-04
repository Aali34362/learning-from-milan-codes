using Application.Data;
using Domain.Customers;
using MediatR;

namespace Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(new CustomerId(Guid.NewGuid()), request.Email, request.Email);

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
