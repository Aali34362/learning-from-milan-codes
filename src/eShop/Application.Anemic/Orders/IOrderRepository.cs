using Domain.Anemic.Orders;

namespace Application.Anemic.Orders;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);

    void Add(Order order);
}