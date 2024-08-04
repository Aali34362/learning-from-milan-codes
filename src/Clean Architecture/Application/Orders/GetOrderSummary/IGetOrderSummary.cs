using Domain.Orders;

namespace Application.Orders.GetOrderSummary;

public interface IGetOrderSummary
{
    Task<OrderSummary?> ExecuteAsync(Guid id);
}