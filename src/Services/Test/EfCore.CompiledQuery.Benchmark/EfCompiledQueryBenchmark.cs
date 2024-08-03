using BenchmarkDotNet.Attributes;

namespace EfCore.CompiledQuery.Benchmark;

[MemoryDiagnoser]
public class EfCompiledQueryBenchmark
{
    private const long CustomerId = 7000;
    private const string Name = "Milan";
    private const int Age = 28;

    //[Benchmark]
    //public Customer? GetCustomerById()
    //{
    //    using var dbContext = new AppDbContext();

    //    return dbContext.GetCustomerById(CustomerId);
    //}

    //[Benchmark]
    //public Customer? GetCustomerByIdCompiled()
    //{
    //    using var dbContext = new AppDbContext();

    //    return dbContext.GetCustomerByIdCompiled(CustomerId);
    //}

    //[Benchmark]
    //public Customer? GetCustomerByIdNoTracking()
    //{
    //    using var dbContext = new AppDbContext();

    //    return dbContext.GetCustomerByIdNoTracking(CustomerId);
    //}

    //[Benchmark]
    //public Customer? GetCustomerByIdNoTrackingCompiled()
    //{
    //    using var dbContext = new AppDbContext();

    //    return dbContext.GetCustomerByIdNoTrackingCompiled(CustomerId);
    //}

    [Benchmark]
    public async Task<Customer?> GetCustomerByNameAndAgeAsync()
    {
        using var dbContext = new AppDbContext();

        return await dbContext.GetCustomerByNameAndAgeAsync(Name, Age);
    }

    [Benchmark]
    public async Task<Customer?> GetCustomerByNameAndAgeCompiledAsync()
    {
        using var dbContext = new AppDbContext();

        return await dbContext.GetCustomerByNameAndAgeCompiledAsync(Name, Age);
    }
}
