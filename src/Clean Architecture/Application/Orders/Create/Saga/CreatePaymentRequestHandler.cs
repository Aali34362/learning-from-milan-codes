using Application.Abstractions;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace Application.Orders.Create.Saga;

internal sealed class CreatePaymentRequestHandler
    : IHandleMessages<CreateOrderPaymentRequest>
{
    private readonly IPaymentService _paymentService;
    private readonly IBus _bus;
    private readonly ILogger<CreatePaymentRequestHandler> _logger;

    public CreatePaymentRequestHandler(
        IPaymentService paymentService,
        IBus bus,
        ILogger<CreatePaymentRequestHandler> logger)
    {
        _paymentService = paymentService;
        _bus = bus;
        _logger = logger;
    }

    public async Task Handle(CreateOrderPaymentRequest message)
    {
        try
        {
            _logger.LogInformation("Creating payment request {@OrderId}", message.OrderId);

            await _paymentService.TryCreatePaymentRequest(message.OrderId);

            _logger.LogInformation("Payment request created {@OrderId}", message.OrderId);

            await _bus.Publish(new OrderPaymentRequestCreatedEvent(message.OrderId));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Payment request failed {@OrderId}", message.OrderId);

            await _bus.Publish(new OrderPaymentRequestFailedEvent(message.OrderId));
        }
    }
}
