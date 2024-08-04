using Application.Data;
using Domain.Customers;
using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderSummaryRepository _orderSummaryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IOrderSummaryRepository orderSummaryRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _orderSummaryRepository = orderSummaryRepository;
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

        _orderSummaryRepository.Add(new OrderSummary(order.Id.Value, customer.Id.Value, 0));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
