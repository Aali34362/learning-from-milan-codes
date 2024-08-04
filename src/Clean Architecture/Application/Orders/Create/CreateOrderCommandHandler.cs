using Domain.Customers;
using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
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
    }
}
