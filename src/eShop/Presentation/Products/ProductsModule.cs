using Application.Products.CreateProduct;
using Application.Products.DeleteProduct;
using Application.Products.GetProducts;
using Application.Products.UpdateProduct;
using Carter;
using Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Products;

public class ProductsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (long cursor, int pageSize, ISender sender) =>
        {
            var result = await sender
                .Send(new GetProductsCursorQuery(cursor, pageSize));

            return Results.Ok(result.Value);
        });

        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            CreateProductCommand command = request.Adapt<CreateProductCommand>();

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapPut(
            "/products/{productId}",
            async (
                long productId,
                [FromBody] UpdateProductRequest request,
                ISender sender) =>
            {
                UpdateProductCommand command = request.Adapt<UpdateProductCommand>() with
                {
                    Id = productId
                };

                Result result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.NotFound(result.Error);
                }

                return Results.NoContent();
            });

        app.MapDelete("/products/{productId}", async (long productId, ISender sender) =>
        {
            await sender.Send(new DeleteProductCommand(productId));

            return Results.NoContent();
        });
    }
}
