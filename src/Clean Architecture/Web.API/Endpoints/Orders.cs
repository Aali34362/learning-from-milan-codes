using Application.Orders.AddLineItem;
using Application.Orders.Create;
using Application.Orders.GetOrderSummary;
using Application.Orders.RemoveLineItem;
using Carter;
using Domain.Orders;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints;

public class Orders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (Guid customerId, ISender sender) =>
        {
            var command = new CreateOrderCommand(customerId);

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapPut("orders/{id}/line-items", async (
            Guid id,
            [FromBody] AddLineItemRequest request,
            ISender sender) =>
        {
            var command = new AddLineItemCommand(
                new OrderId(id),
                new ProductId(request.ProductId),
                request.Currency,
                request.Amount);

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapDelete("orders/{id}/line-items/{lineItemId}", async (Guid id, Guid lineItemId, ISender sender) =>
        {
            var command = new RemoveLineItemCommand(new OrderId(id), new LineItemId(lineItemId));

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapGet("orders/{id}/summary", async (Guid id, ISender sender) =>
        {
            var query = new GetOrderSummaryQuery(id);

            return Results.Ok(await sender.Send(query));
        });
    }
}
