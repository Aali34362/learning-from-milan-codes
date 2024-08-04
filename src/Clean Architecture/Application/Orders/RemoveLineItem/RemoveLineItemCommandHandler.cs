using Domain.Orders;
using MediatR;

namespace Application.Orders.RemoveLineItem;

internal sealed class RemoveLineItemCommandHandler : IRequestHandler<RemoveLineItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public RemoveLineItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);

        if (order is null)
        {
            return;
        }

        order.RemoveLineItem(request.LineItemId);
    }
}
