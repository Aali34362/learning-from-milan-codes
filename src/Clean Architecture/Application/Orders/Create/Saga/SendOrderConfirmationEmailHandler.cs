using Application.Abstractions;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace Application.Orders.Create.Saga;

internal sealed class SendOrderConfirmationEmailHandler
    : IHandleMessages<SendOrderConfirmationEmail>
{
    private readonly IEmailService _emailService;
    private readonly IBus _bus;
    private readonly ILogger<SendOrderConfirmationEmailHandler> _logger;

    public SendOrderConfirmationEmailHandler(
        IEmailService emailService,
        IBus bus,
        ILogger<SendOrderConfirmationEmailHandler> logger)
    {
        _emailService = emailService;
        _bus = bus;
        _logger = logger;
    }

    public async Task Handle(SendOrderConfirmationEmail message)
    {
        _logger.LogInformation("Sending order confirmation {@OrderId}", message.OrderId);

        await _emailService.SendOrderConfirmationAsync(message.OrderId);

        _logger.LogInformation("Order confirmation sent {@OrderId}", message.OrderId);

        await _bus.Publish(new OrderConfirmationEmailSentEvent(message.OrderId));
    }
}
