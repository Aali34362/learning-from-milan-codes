using Application.Customers.Create;
using Carter;
using FluentValidation;
using MediatR;

namespace Web.API.Endpoints;

public sealed class Customers : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("customers", async (
            CreateCustomerRequest request,
            IValidator<CreateCustomerCommand> validator,
            ISender sender) =>
        {
            var command = new CreateCustomerCommand(request.Email, request.Name);

            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            await sender.Send(command);

            return Results.Ok();
        });
    }
}
