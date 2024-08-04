using Application.Orders.GetOrder;
using Domain.Orders;

namespace Application.Orders;

public interface IOrderReadService
{
    Task<OrderResponse?> GetByIdAsync(OrderId id);

    Task<OrderSummary?> GetSummaryByIdAsync(Guid id);
}
