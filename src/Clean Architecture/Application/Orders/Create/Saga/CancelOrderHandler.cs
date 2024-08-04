using Application.Data;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Rebus.Bus;
using Rebus.Handlers;

namespace Application.Orders.Create.Saga;

internal sealed class CancelOrderHandler : IHandleMessages<CancelOrder>
{
    private readonly IApplicationDbContext _context;
    private readonly IBus _bus;

    public CancelOrderHandler(IApplicationDbContext context, IBus bus)
    {
        _context = context;
        _bus = bus;
    }

    public async Task Handle(CancelOrder message)
    {
        var order = await _context.Orders.SingleAsync(
            o => o.Id == new OrderId(message.OrderId));

        order.Cancel();

        await _context.SaveChangesAsync();

        await _bus.Publish(new OrderCancelledEvent(message.OrderId));
    }
}
