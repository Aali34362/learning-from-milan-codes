using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace Application.Orders.Create;

internal sealed class CreatePaymentRequestHandler
    : IHandleMessages<CreateOrderPaymentRequest>
{
    private readonly ILogger<CreatePaymentRequestHandler> _logger;
    private readonly IBus _bus;

    public CreatePaymentRequestHandler(ILogger<CreatePaymentRequestHandler> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task Handle(CreateOrderPaymentRequest message)
    {
        _logger.LogInformation("Starting payment request {@OrderId}", message.OrderId);

        await Task.Delay(2000);

        _logger.LogInformation("Payment request started {@OrderId}", message.OrderId);

        await _bus.Send(new OrderPaymentRequestSent(message.OrderId));
    }
}
