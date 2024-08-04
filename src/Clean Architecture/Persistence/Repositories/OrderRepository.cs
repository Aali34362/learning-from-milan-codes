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

    public Task<Order?> GetByIdAsync(OrderId id)
    {
        return _context.Orders
            .Include(o => o.LineItems)
            .SingleOrDefaultAsync(o => o.Id == id);
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }
}
