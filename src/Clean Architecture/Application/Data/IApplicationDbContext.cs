using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }

    DbSet<Order> Orders { get; set; }

    DbSet<LineItem> LineItems { get; set; }

    DbSet<OrderSummary> OrderSummaries { get; set; }

    DbSet<Product> Products { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
