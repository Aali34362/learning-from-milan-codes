using Application.Data;
using Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.GetOrder;

internal sealed class GetOrderQueryHandler :
    IRequestHandler<GetOrderQuery, OrderResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IRepository<Order> _ordersRepository;

    public GetOrderQueryHandler(IApplicationDbContext context, IRepository<Order> ordersRepository)
    {
        _context = context;
        _ordersRepository = ordersRepository;
    }

    public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var orderResponse = await _ordersRepository
            .GetQueryable()
            .Where(o => o.Id == new OrderId(request.OrderId))
            .Select(o => new OrderResponse(
                o.Id.Value,
                o.CustomerId.Value,
                o.LineItems
                    .Select(li => new LineItemResponse(li.Id.Value, li.Price.Amount))
                    .ToList()))
            .SingleAsync(cancellationToken);

        return orderResponse;
    }
}
