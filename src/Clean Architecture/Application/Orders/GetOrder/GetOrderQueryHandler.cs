using Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.GetOrder;

internal sealed class GetOrderQueryHandler :
    IRequestHandler<GetOrderQuery, OrderResponse>
{
    private readonly IOrderReadService _orderReadService;

    public GetOrderQueryHandler(IOrderReadService orderReadService)
    {
        _orderReadService = orderReadService;
    }

    public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);

        var orderResponse = await _orderReadService.GetByIdAsync(orderId);

        if (orderResponse is null)
        {
            throw new OrderNotFoundException(orderId);
        }

        return orderResponse;
    }
}
