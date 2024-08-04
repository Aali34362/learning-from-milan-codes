using Application.Abstractions;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace Application.Orders.Create.Saga;

internal class ReserveOrderStockHandler : IHandleMessages<ReserveOrderStock>
{
    private readonly IStockService _stockService;
    private readonly IBus _bus;
    private readonly ILogger<ReserveOrderStockHandler> _logger;

    public ReserveOrderStockHandler(
        IStockService stockService,
        IBus bus,
        ILogger<ReserveOrderStockHandler> logger)
    {
        _stockService = stockService;
        _bus = bus;
        _logger = logger;
    }

    public async Task Handle(ReserveOrderStock message)
    {
        _logger.LogInformation("Reserving order stock {@OrderId}", message.OrderId);

        await _stockService.ReserveForOrderAsync(message.OrderId);

        _logger.LogInformation("Order stock reserved {@OrderId}", message.OrderId);

        await _bus.Publish(new OrderStockReservedEvent(message.OrderId));
    }
}
