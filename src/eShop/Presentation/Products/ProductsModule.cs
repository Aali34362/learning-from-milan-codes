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
using Presentation.Authentication;

namespace Presentation.Products;

[ApiKey]
public class ProductsModule : ICarterModule
{
    [ApiKey]
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            Result<List<ProductResponse>> result = await sender
                .Send(new GetProductsQuery());

            return Results.Ok(result.Value);
        });

        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            CreateProductCommand command = request.Adapt<CreateProductCommand>();

            await sender.Send(command);

            return Results.Ok();
        }).AddEndpointFilter<ApiKeyAuthenticationEndpointFilter>();

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
