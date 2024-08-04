using EfficientDeleting.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace EfficientDeleting.Api;

public static class ProductEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (string name, CatalogDbContext context) =>
        {
            context.Products.Add(new Product { Name = name });

            await context.SaveChangesAsync();

            return Results.Ok();
        });

        app.MapDelete("products/{id}", async (int id, CatalogDbContext context) =>
        {
            var entityEntry = context.Products.Attach(new Product { Id = id });
            entityEntry.State = EntityState.Deleted;

            var saveChanges = context.SaveChanges();

            return Results.NoContent();
        });
    }
}
