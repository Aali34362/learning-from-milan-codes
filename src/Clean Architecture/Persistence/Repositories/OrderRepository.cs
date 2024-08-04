using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrderRepository : Repository<Order, OrderId>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public override Task<Order?> GetByIdAsync(OrderId id)
    {
        return DbContext.Orders
            .Include(o => o.LineItems)
            .SingleOrDefaultAsync(o => o.Id == id);
    }
}
