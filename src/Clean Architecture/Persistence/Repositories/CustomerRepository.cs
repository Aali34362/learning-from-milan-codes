using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Customer?> GetByIdAsync(CustomerId id)
    {
        return _context.Customers
            .SingleOrDefaultAsync(c => c.Id == id);
    }
}
