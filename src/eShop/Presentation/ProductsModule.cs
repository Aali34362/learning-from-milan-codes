using Application.Products.CreateProduct;
using Application.Products.GetProducts;
using Carter;
using Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation;

public class ProductsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/products",
            async (ISender sender) =>
            {
                Result<List<ProductResponse>> result = await sender.Send(new GetProductsQuery());

                return Results.Ok(result.Value);
            });

        app.MapPost(
            "/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                CreateProductCommand command = request.Adapt<CreateProductCommand>();

                await sender.Send(command);

                return Results.Ok();
            });
    }
}

public sealed record CreateProductRequest(
    string Name,
    decimal Price,
    List<string> Tags);
