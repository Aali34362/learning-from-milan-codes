using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.GetById;
using Application.Products.Update;
using Carter;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

public class ProductsEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products");

        group.MapPost("", CreateProduct);

        group.MapGet("", GetProducts);

        group.MapGet("{id}", GetProduct).WithName(nameof(GetProduct));

        group.MapPut("{id}", UpdateProduct).WithName(nameof(UpdateProduct));

        group.MapDelete("{id}", DeleteProduct).WithName(nameof(DeleteProduct));
    }

    public static async Task<IResult> CreateProduct(
        CreateProductRequest request,
        ISender sender)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Sku,
            request.Currency,
            request.Amount);

        await sender.Send(command);

        return Results.Ok();
    }

    public static async Task<IResult> GetProducts(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        ISender sender)
    {
        var query = new GetProductsQuery(searchTerm, sortColumn, sortOrder, page, pageSize);

        var products = await sender.Send(query);

        return Results.Ok(products);
    }

    public static async Task<Results<Ok<ProductResponse>, NotFound<string>>> GetProduct(
        Guid id,
        ISender sender)
    {
        try
        {
            var productResponse = await sender.Send(new GetProductQuery(new ProductId(id)));

            return TypedResults.Ok(productResponse);
        }
        catch (ProductNotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }

    public static async Task<IResult> UpdateProduct(
        Guid id,
        [FromBody] UpdateProductRequest request,
        ISender sender)
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
    }

    public static async Task<IResult> DeleteProduct(
        Guid id,
        ISender sender)
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
    }
}
