using Application.Orders.AddLineItem;
using Ardalis.ApiEndpoints;
using Domain.Orders;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints.Orders;

public class AddLineItem : EndpointBaseAsync
    .WithRequest<AddLineItemApiRequest>
    .WithActionResult
{
    private readonly ISender _sender;

    public AddLineItem(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("api/orders/{id}/line-items")]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] AddLineItemApiRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new AddLineItemCommand(
            new OrderId(request.OrderId),
            new ProductId(request.Request.ProductId),
            request.Request.Currency,
            request.Request.Amount);

        await _sender.Send(command);

        return Ok();
    }
}