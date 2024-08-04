using Microsoft.EntityFrameworkCore;

namespace EfficientDeleting.Api.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}