using Application.Orders;
using Application.Orders.GetOrder;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

internal sealed class OrderReadService : IOrderReadService
{
    private readonly ApplicationDbContext _context;

    public OrderReadService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderResponse?> GetByIdAsync(OrderId id)
    {
        var orderResponse = await _context
            .Orders
            .Where(o => o.Id == id)
            .Select(o => new OrderResponse(
                o.Id.Value,
                o.CustomerId.Value,
                o.LineItems
                    .Select(li => new LineItemResponse(li.Id.Value, li.Price.Amount))
                    .ToList()))
            .FirstOrDefaultAsync();

        return orderResponse;
    }

    public async Task<OrderSummary?> GetSummaryByIdAsync(Guid id)
    {
        return await _context.OrderSummaries
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
