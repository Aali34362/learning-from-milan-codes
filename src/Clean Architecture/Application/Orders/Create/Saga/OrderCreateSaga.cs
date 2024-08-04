using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace Application.Orders.Create.Saga;

public class OrderCreateSaga : Saga<OrderCreateSagaData>,
    IAmInitiatedBy<OrderCreatedEvent>,
    IHandleMessages<OrderConfirmationEmailSentEvent>,
    IHandleMessages<OrderStockReservedEvent>,
    IHandleMessages<OrderStockReleasedEvent>,
    IHandleMessages<OrderPaymentRequestCreatedEvent>,
    IHandleMessages<OrderPaymentRequestFailedEvent>,
    IHandleMessages<OrderCancelledEvent>
{
    private readonly IBus _bus;

    public OrderCreateSaga(IBus bus)
    {
        _bus = bus;
    }

    protected override void CorrelateMessages(ICorrelationConfig<OrderCreateSagaData> config)
    {
        config.Correlate<OrderCreatedEvent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderConfirmationEmailSentEvent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderStockReservedEvent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderStockReleasedEvent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderPaymentRequestCreatedEvent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderPaymentRequestFailedEvent>(m => m.OrderId, s => s.OrderId);
        config.Correlate<OrderCancelledEvent>(m => m.OrderId, s => s.OrderId);
    }

    public async Task Handle(OrderCreatedEvent message)
    {
        if (!IsNew)
        {
            return;
        }

        await _bus.Send(new SendOrderConfirmationEmail(message.OrderId));
    }

    public async Task Handle(OrderConfirmationEmailSentEvent message)
    {
        Data.ConfirmationEmailSent = true;

        await _bus.Send(new ReserveOrderStock(message.OrderId));
    }

    public async Task Handle(OrderStockReservedEvent message)
    {
        Data.StockReserved = true;

        await _bus.Send(new CreateOrderPaymentRequest(message.OrderId));
    }

    public Task Handle(OrderPaymentRequestCreatedEvent message)
    {
        Data.PaymentRequestSent = true;

        MarkAsComplete();

        return Task.CompletedTask;
    }

    public async Task Handle(OrderPaymentRequestFailedEvent message)
    {
        if (Data.StockReserved)
        {
            await _bus.Send(new ReleaseOrderStock(message.OrderId));
        }
    }

    public async Task Handle(OrderStockReleasedEvent message)
    {
        Data.StockReserved = false;

        await _bus.Send(new CancelOrder(message.OrderId));
    }

    public Task Handle(OrderCancelledEvent message)
    {
        MarkAsComplete();

        return Task.CompletedTask;
    }
}
