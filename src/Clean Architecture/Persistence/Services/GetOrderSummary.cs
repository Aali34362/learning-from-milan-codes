using Application.Orders.GetOrderSummary;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

internal sealed class GetOrderSummary : IGetOrderSummary
{
    private readonly ApplicationDbContext _context;

    public GetOrderSummary(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderSummary?> ExecuteAsync(Guid id)
    {
        return await _context.OrderSummaries
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
