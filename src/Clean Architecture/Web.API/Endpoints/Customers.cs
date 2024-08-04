using Application.Customers.Create;
using Carter;
using MediatR;

namespace Web.API.Endpoints;

public sealed class Customers : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("customers", async (
            CreateCustomerRequest request,
            ISender sender) =>
        {
            var command = new CreateCustomerCommand(request.Email, request.Name);

            await sender.Send(command);

            return Results.Ok();
        });
    }
}
