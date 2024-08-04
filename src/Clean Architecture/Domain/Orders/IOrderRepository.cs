namespace Domain.Orders;

public interface IOrderRepository
{
    Task<Order?> GetByIdWithLineItemAsync(OrderId id, LineItemId lineItemId);

    bool HasOneLineItem(Order order);

    void Add(Order order);
}