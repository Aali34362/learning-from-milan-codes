using AwesomeCompany;
using AwesomeCompany.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPut("increase-salaries", async (int companyId, DatabaseContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .Include(c => c.Employees)
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound(
            $"The company with Id '{companyId}' was not found.");
    }

    foreach (var employee in company.Employees)
    {
        employee.Salary *= 1.1m;
    }

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("increase-salaries-v2", async (int companyId, DatabaseContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound(
            $"The company with Id '{companyId}' was not found.");
    }

    await dbContext.Set<Employee>()
        .Where(e => e.CompanyId == company.Id)
        .ExecuteUpdateAsync(s => s.SetProperty(
            e => e.Salary,
            e => e.Salary * 1.1m));

    return Results.NoContent();
});

app.MapDelete("delete-employees", async (
    int companyId,
    decimal salaryThreshold,
    DatabaseContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound(
            $"The company with Id '{companyId}' was not found.");
    }

    await dbContext.Set<Employee>()
        .Where(e => e.CompanyId == company.Id &&
                    e.Salary > salaryThreshold)
        .ExecuteDeleteAsync();

    return Results.NoContent();
});

app.Run();
