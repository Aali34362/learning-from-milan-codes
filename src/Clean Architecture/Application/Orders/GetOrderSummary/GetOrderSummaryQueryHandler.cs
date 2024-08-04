using Domain.Orders;
using MediatR;

namespace Application.Orders.GetOrderSummary;

internal sealed class GetOrderSummaryQueryHandler
    : IRequestHandler<GetOrderSummaryQuery, OrderSummary?>
{
    private readonly IGetOrderSummary _getOrderSummary;

    public GetOrderSummaryQueryHandler(IGetOrderSummary getOrderSummary)
    {
        _getOrderSummary = getOrderSummary;
    }

    public async Task<OrderSummary?> Handle(GetOrderSummaryQuery request, CancellationToken cancellationToken)
    {
        return await _getOrderSummary.ExecuteAsync(request.OrderId);
    }
}
