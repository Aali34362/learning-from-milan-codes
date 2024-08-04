using EfficientDeleting.Api;
using EfficientDeleting.Api.Data;
using EfficientDeleting.Api.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogDbContext>(options =>
    options
        .UseNpgsql(builder.Configuration.GetConnectionString("Database"))
        .UseSnakeCaseNamingConvention());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

ProductEndpoints.Map(app);

app.UseHttpsRedirection();

app.Run();
