namespace Application.Orders.Create.Saga;

public record OrderCreatedEvent(Guid OrderId);

public record CancelOrder(Guid OrderId);

public record OrderCancelledEvent(Guid OrderId);

// Confirmation
public record SendOrderConfirmationEmail(Guid OrderId);

public record OrderConfirmationEmailSentEvent(Guid OrderId);

// Stock
public record ReserveOrderStock(Guid OrderId);

public record OrderStockReservedEvent(Guid OrderId);

public record ReleaseOrderStock(Guid OrderId);

public record OrderStockReleasedEvent(Guid OrderId);

// Payment request
public record CreateOrderPaymentRequest(Guid OrderId);

public record OrderPaymentRequestCreatedEvent(Guid OrderId);

public record OrderPaymentRequestFailedEvent(Guid OrderId);