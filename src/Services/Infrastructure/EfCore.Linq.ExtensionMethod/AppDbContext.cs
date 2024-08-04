using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCore.Linq.ExtensionMethod;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=linq-extension-method-sample;Trusted_Connection=True;TrustServerCertificate=True;");

        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    public DbSet<Customer> Customers { get; set; }
}