using Domain.Orders;

namespace Application.Orders.Create;

public interface ICalculateOrderSummary
{
    Task<OrderSummary?> CalculateAsync(OrderId orderId);
}