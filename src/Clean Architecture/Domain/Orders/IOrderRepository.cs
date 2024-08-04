namespace Domain.Orders;

public interface IOrderRepository
{
    Task<Order?> GetByIdWithLineItemAsync(OrderId id, LineItemId lineItemId);

    void Add(Order order);
}