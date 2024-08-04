using Application.Data;
using Domain.Orders;
using MediatR;

namespace Application.Orders.RemoveLineItem;

internal sealed class RemoveLineItemCommandHandler : IRequestHandler<RemoveLineItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveLineItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
        {
            return;
        }

        order.RemoveLineItem(request.LineItemId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
