using AwesomeCompany;
using AwesomeCompany.Entities;
using AwesomeCompany.Models;
using AwesomeCompany.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.AddDbContext<DatabaseContext>(
    (serviceProvider, dbContextOptionsBuilder) =>
    {
        var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

        dbContextOptionsBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
        {
            sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);

            sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
        });

        dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);

        dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("companies/{companyId:int}", async (int companyId, DatabaseContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound($"The company with Id '{companyId}' was not found.");
    }

    var response = new CompanyResponse(company.Id, company.Name);

    return Results.Ok(response);
});

app.Run();