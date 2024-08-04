using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.GetById;
using Application.Products.Update;
using Carter;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Web.API.Endpoints;

public class Products : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (CreateProductCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });

        app.MapGet("products", async (
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            ISender sender) =>
        {
            var query = new GetProductsQuery(searchTerm, sortColumn, sortOrder, page, pageSize);

            var products = await sender.Send(query);

            return Results.Ok(products);
        });

        app.MapGet("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetProductQuery(new ProductId(id))));
            }
            catch (ProductNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapPut("products/{id:guid}", async (Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            var command = new UpdateProductCommand(
                new ProductId(id),
                request.Name,
                request.Sku,
                request.Currency,
                request.Amount);
            
            await sender.Send(command);

            return Results.NoContent();
        });

        app.MapDelete("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteProductCommand(new ProductId(id)));

                return Results.NoContent();
            }
            catch (ProductNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });
    }
}
