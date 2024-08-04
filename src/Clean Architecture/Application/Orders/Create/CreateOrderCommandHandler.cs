using Application.Data;
using Domain.Customers;
using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;
    private readonly IRepository<Customer> _customersRepository;
    private readonly IRepository<Order> _ordersRepository;

    public CreateOrderCommandHandler(
        IApplicationDbContext context,
        IPublisher publisher,
        IRepository<Customer> customersRepository,
        IRepository<Order> ordersRepository)
    {
        _context = context;
        _publisher = publisher;
        _customersRepository = customersRepository;
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customersRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
        {
            return;
        }

        var order = Order.Create(customer.Id);

        _ordersRepository.Insert(order);

        await _ordersRepository.SaveChangesAsync();

        await _publisher.Publish(new OrderCreatedEvent(order.Id), cancellationToken);
    }
}
