using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WebShop.Api.Contracts;
using WebShop.Api.Orders;
using WebShop.Api.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<OrdersDbContext>(
    options => options.UseSqlServer(
        connectionString,
        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "orders")));

builder.Services.AddDbContext<ProductsDbContext>(
    options => options.UseSqlServer(
        connectionString,
        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "products")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("products", async (ProductsDbContext productsContext) =>
{
    return Results.Ok(await productsContext.Products.Select(p => p.Id).ToArrayAsync());
});

app.MapPost("orders", async (
    SubmitOrderRequest request,
    ProductsDbContext productsContext,
    OrdersDbContext ordersContext) =>
{
    var products = await productsContext.Products
        .Where(p => request.ProductIds.Contains(p.Id))
        .AsNoTracking()
        .ToListAsync();

    if (products.Count != request.ProductIds.Count)
    {
        return Results.BadRequest("Some product is missing");
    }

    var order = new Order
    {
        Id = Guid.NewGuid(),
        TotalPrice = products.Sum(p => p.Price),
        LineItems = products
            .Select(p => new LineItem
            {
                Id = Guid.NewGuid(),
                ProductId = p.Id,
                Price = p.Price
            })
            .ToList()
    };

    ordersContext.Orders.Add(order);

    await ordersContext.SaveChangesAsync();

    return Results.Ok(order);
});

app.UseHttpsRedirection();

app.Run();
