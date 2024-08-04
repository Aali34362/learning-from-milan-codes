namespace Domain.Orders;

public record OrderSummary(Guid Id, Guid CustomerId, decimal TotalPrice);
