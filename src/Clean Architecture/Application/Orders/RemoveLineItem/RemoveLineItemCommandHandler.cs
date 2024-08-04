using Application.Data;
using Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.RemoveLineItem;

internal sealed class RemoveLineItemCommandHandler : IRequestHandler<RemoveLineItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveLineItemCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdWithLineItemAsync(
            request.OrderId,
            request.LineItemId);

        if (order is null)
        {
            return;
        }

        order.RemoveLineItem(request.LineItemId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
