using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Order?> GetByIdWithLineItemAsync(OrderId id, LineItemId lineItemId)
    {
        return _context.Orders
            .Include(o => o.LineItems.Where(li => li.Id == lineItemId))
            .SingleOrDefaultAsync(o => o.Id == id);
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }
}
