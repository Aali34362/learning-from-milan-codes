using Application.Data;
using Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.GetOrder;

internal sealed class GetOrderQueryHandler :
    IRequestHandler<GetOrderQuery, OrderResponse>
{
    private readonly IApplicationDbContext _context;

    public GetOrderQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);

        var orderResponse = await _context
            .Orders
            .Where(o => o.Id == orderId)
            .Select(o => new OrderResponse(
                o.Id.Value,
                o.CustomerId.Value,
                o.LineItems
                    .Select(li => new LineItemResponse(li.Id.Value, li.Price.Amount))
                    .ToList()))
            .FirstOrDefaultAsync(cancellationToken);

        if (orderResponse is null)
        {
            throw new OrderNotFoundException(orderId);
        }

        return orderResponse;
    }
}
