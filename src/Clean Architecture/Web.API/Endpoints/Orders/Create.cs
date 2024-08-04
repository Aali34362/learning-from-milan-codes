using Application.Orders.Create;
using Carter;
using MediatR;

namespace Web.API.Endpoints.Orders;

public class Create : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (Guid customerId, ISender sender) =>
        {
            var command = new CreateOrderCommand(customerId);

            await sender.Send(command);

            return Results.Ok();
        });
    }
}
