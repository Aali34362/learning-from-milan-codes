using Application.Data;
using Domain.Customers;
using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public CreateOrderCommandHandler(IApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(
            new CustomerId(request.CustomerId));

        if (customer is null)
        {
            return;
        }

        var order = Order.Create(customer.Id);

        _context.Orders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new OrderCreatedEvent(order.Id), cancellationToken);
    }
}
