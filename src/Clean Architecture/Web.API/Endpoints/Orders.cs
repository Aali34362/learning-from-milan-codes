using Application.Orders.Create;
using Application.Orders.GetOrderSummary;
using Application.Orders.RemoveLineItem;
using Carter;
using Domain.Orders;
using MediatR;

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
