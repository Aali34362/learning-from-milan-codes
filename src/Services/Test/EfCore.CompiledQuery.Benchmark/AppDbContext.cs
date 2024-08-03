using Bogus;
using Microsoft.EntityFrameworkCore;

namespace EfCore.CompiledQuery.Benchmark;

internal class AppDbContext : DbContext
{
    private static readonly Func<AppDbContext, long, Customer?> GetById =
        Microsoft.EntityFrameworkCore.EF.CompileQuery(
            (AppDbContext context, long id) =>
                context.Set<Customer>().FirstOrDefault(c => c.Id == id));

    private static readonly Func<AppDbContext, long, Customer?> GetByIdNoTracking =
        Microsoft.EntityFrameworkCore.EF.CompileQuery(
            (AppDbContext context, long id) =>
                context.Set<Customer>().AsNoTracking().FirstOrDefault(c => c.Id == id));

    private static readonly Func<AppDbContext, string, int, Task<Customer?>> GetByNameAndAge =
        Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery(
            (AppDbContext context, string name, int age) =>
                context.Set<Customer>().FirstOrDefault(c => c.Name == name && c.Age == age));

    public Customer? GetCustomerById(long id)
    {
        return Set<Customer>().FirstOrDefault(c => c.Id == id);
    }

    public Customer? GetCustomerByIdCompiled(long id)
    {
        return GetById(this, id);
    }

    public Customer? GetCustomerByIdNoTracking(long id)
    {
        return Set<Customer>().AsNoTracking().FirstOrDefault(c => c.Id == id);
    }

    public Customer? GetCustomerByIdNoTrackingCompiled(long id)
    {
        return GetByIdNoTracking(this, id);
    }

    public async Task<Customer?> GetCustomerByNameAndAgeAsync(string name, int age)
    {
        return await Set<Customer>()
            .FirstOrDefaultAsync(c => c.Name == name && c.Age == age);
    }

    public async Task<Customer?> GetCustomerByNameAndAgeCompiledAsync(string name, int age)
    {
        return await GetByNameAndAge(this, name, age);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(builder =>
        {
            var faker = new Faker();
            var customers = new List<Customer>();
            for (var i = 0; i < 10_000; i++)
            {
                customers.Add(new Customer
                {
                    Id = i + 1,
                    Age = faker.Random.Number(10, 100),
                    Name = faker.Name.FullName()
                });
            }

            builder.HasData(customers);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=ef-compiled-query-benchmark;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}