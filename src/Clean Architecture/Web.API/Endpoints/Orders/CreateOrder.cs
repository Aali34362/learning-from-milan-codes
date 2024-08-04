using Application.Orders.Create;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints.Orders;

public class CreateOrder : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly ISender _sender;

    public CreateOrder(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("api/orders")]
    public override async Task<ActionResult> HandleAsync(
        [FromQuery(Name = "customerId")] Guid request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateOrderCommand(request);

        await _sender.Send(command);

        return Ok();
    }
}