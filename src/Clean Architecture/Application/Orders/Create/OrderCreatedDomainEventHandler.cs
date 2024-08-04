using Application.Data;
using Domain.Orders;
using MediatR;

namespace Application.Orders.Create;

internal sealed class OrderCreatedDomainEventHandler
    : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly ICalculateOrderSummary _calculateOrderSummary;
    private readonly IOrderSummaryRepository _orderSummaryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderCreatedDomainEventHandler(
        ICalculateOrderSummary calculateOrderSummary,
        IOrderSummaryRepository orderSummaryRepository,
        IUnitOfWork unitOfWork)
    {
        _calculateOrderSummary = calculateOrderSummary;
        _orderSummaryRepository = orderSummaryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var orderSummary = await _calculateOrderSummary.CalculateAsync(notification.OrderId);

        if (orderSummary is null)
        {
            return;
        }

        _orderSummaryRepository.Add(orderSummary);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
