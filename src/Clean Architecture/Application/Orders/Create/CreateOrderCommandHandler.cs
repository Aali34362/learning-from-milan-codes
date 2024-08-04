using Application.Abstractions.Data;
using Domain.Customers;
using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(
            new CustomerId(request.CustomerId));

        if (customer is null)
        {
            return;
        }

        var order = Order.Create(customer.Id);

        _orderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
