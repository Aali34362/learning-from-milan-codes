namespace Application.Orders.Create;

public record OrderCreatedEvent(Guid OrderId);

public record OrderConfirmationEmailSent(Guid OrderId);

public record OrderPaymentRequestSent(Guid OrderId);

public record SendOrderConfirmationEmail(Guid OrderId);

public record CreateOrderPaymentRequest(Guid OrderId);
