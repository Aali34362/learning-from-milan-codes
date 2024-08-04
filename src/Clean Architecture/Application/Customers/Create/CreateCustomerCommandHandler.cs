using Application.Data;
using Domain.Customers;
using EntityFramework.Exceptions.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCustomerCommandHandler> _logger;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateCustomerCommandHandler> logger)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = new Customer(new CustomerId(Guid.NewGuid()), request.Email, request.Name);

            _customerRepository.Add(customer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (UniqueConstraintException e)
        {
            _logger.LogError(e.Message, e);

            throw;
        }
    }
}
