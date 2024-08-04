using Domain.Anemic.Orders;
using Domain.Anemic.Products;

namespace Application.Anemic.Orders;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(Guid customerId, CancellationToken cancellationToken = default)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Status = OrderStatus.Pending
        };

        _orderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddLineItem(Guid orderId, Product product, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

        var lineItem = new LineItem
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            ProductId = product.Id,
            Price = product.Price,
            Currency = product.Currency
        };

        order.LineItems.Add(lineItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}