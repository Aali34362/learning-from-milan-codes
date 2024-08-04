using IntegrationEvents;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace Application.Orders.Create;

public class OrderCreateSaga : Saga<OrderCreateSagaData>,
    IAmInitiatedBy<OrderCreatedIntegrationEvent>,
    IHandleMessages<OrderConfirmationEmailSent>,
    IHandleMessages<OrderPaymentRequestSent>
{
    private readonly IBus _bus;

    public OrderCreateSaga(IBus bus)
    {
        _bus = bus;
    }

    protected override void CorrelateMessages(ICorrelationConfig<OrderCreateSagaData> config)
    {
        config.Correlate<OrderCreatedIntegrationEvent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderConfirmationEmailSent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderPaymentRequestSent>(m => m.OrderId, s => s.OrderId);
    }

    public async Task Handle(OrderCreatedIntegrationEvent message)
    {
        if (!IsNew)
        {
            return;
        }

        await _bus.Send(new SendOrderConfirmationEmail(message.OrderId));
    }

    public async Task Handle(OrderConfirmationEmailSent message)
    {
        Data.ConfirmationEmailSent = true;

        await _bus.Send(new CreateOrderPaymentRequest(message.OrderId));
    }

    public Task Handle(OrderPaymentRequestSent message)
    {
        Data.PaymentRequestSent = true;

        MarkAsComplete();

        return Task.CompletedTask;
    }
}
