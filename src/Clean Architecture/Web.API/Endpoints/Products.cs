using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.GetById;
using Application.Products.Update;
using Carter;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints;

public class Products : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Sku,
                request.Currency,
                request.Amount);

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
        }).WithName("GetProducts");

        app.MapGet("products/{id:guid}", async (
            Guid id,
            ISender sender,
            LinkGenerator linkGenerator,
            HttpContext context) =>
        {
            try
            {
                var productResponse = await sender.Send(new GetProductQuery(new ProductId(id)));

                return Results.Ok(productResponse);
            }
            catch (ProductNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithName("GetProduct");

        app.MapPut("products/{id:guid}", async (Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            try
            {
                var command = new UpdateProductCommand(
                    new ProductId(id),
                    request.Name,
                    request.Sku,
                    request.Currency,
                    request.Amount);

                await sender.Send(command);

                return Results.NoContent();
            }
            catch (ProductNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithName("UpdateProduct");

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
        }).WithName("DeleteProduct");
    }
}
