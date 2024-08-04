using Application.Orders.RemoveLineItem;
using Domain.Orders;
using FastEndpoints;
using MediatR;

namespace Web.API.Endpoints.Orders;

public class RemoveLineItem : Endpoint<RemoveLineItemRequest>
{
    private readonly ISender _sender;

    public RemoveLineItem(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete("api/orders/{id}/line-items/{lineItemId}");
    }

    public override async Task HandleAsync(RemoveLineItemRequest req, CancellationToken ct)
    {
        var command = new RemoveLineItemCommand(
            new OrderId(req.OrderId),
            new LineItemId(req.LineItemId));

        await _sender.Send(command);

        await SendNotFoundAsync();
    }
}