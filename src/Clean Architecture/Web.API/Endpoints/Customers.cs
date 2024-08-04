using Application.Customers.Login;
using Application.Customers.Register;
using Carter;
using MediatR;

namespace Web.API.Endpoints;

public sealed class Customers : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("customers", async (
            RegisterCustomerRequest request,
            ISender sender) =>
        {
            var command = new RegisterCustomerCommand(
                request.Email,
                request.Password,
                request.Name);

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapPost("login", async (LoginCommand command, ISender sender) =>
        {
            return Results.Ok(await sender.Send(command));
        });
    }
}
