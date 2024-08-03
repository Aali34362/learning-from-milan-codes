using Application.Products.CreateProduct;
using Application.Products.GetProducts;
using Carter;
using Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Products;

public class ProductsModule : CarterModule
{
    public ProductsModule()
        : base("/products")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (ISender sender) =>
        {
            Result<List<ProductResponse>> result = await sender.Send(new GetProductsQuery());

            return Results.Ok(result.Value);
        });

        app.MapPost("/", async (CreateProductRequest request, ISender sender) =>
        {
            CreateProductCommand command = request.Adapt<CreateProductCommand>();

            await sender.Send(command);

            return Results.Ok();
        });
    }
}
