using Application.Abstractions.Authentication;
using Application.Data;
using Domain.Customers;
using MediatR;

namespace Application.Customers.Register;

internal sealed class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCustomerCommandHandler(
        IAuthenticationService authenticationService,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var identityId = await _authenticationService.RegisterAsync(
            request.Email,
            request.Password);

        var customer = new Customer(
            new CustomerId(Guid.NewGuid()),
            request.Email,
            request.Name,
            identityId);

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
