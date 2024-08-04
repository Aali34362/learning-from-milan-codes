using Application.Abstractions;
using Rebus.Bus;
using Rebus.Handlers;

namespace Application.Orders.Create.Saga;

internal sealed class ReleaseOrderStockHandler : IHandleMessages<ReleaseOrderStock>
{
    private readonly IStockService _stockService;
    private readonly IBus _bus;

    public ReleaseOrderStockHandler(IStockService stockService, IBus bus)
    {
        _stockService = stockService;
        _bus = bus;
    }

    public async Task Handle(ReleaseOrderStock message)
    {
        await _stockService.ReleaseForOrderAsync(message.OrderId);

        await _bus.Publish(new OrderStockReleasedEvent(message.OrderId));
    }
}
