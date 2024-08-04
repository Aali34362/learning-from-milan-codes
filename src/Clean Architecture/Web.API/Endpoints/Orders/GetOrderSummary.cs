using Application.Orders.GetOrderSummary;
using Domain.Orders;
using FastEndpoints;
using MediatR;

namespace Web.API.Endpoints.Orders;

public class GetOrderSummary : Endpoint<GetOrderSummaryRequest, OrderSummary>
{
    private readonly ISender _sender;

    public GetOrderSummary(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("api/orders/{id}/summary");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetOrderSummaryRequest req, CancellationToken ct)
    {
        var query = new GetOrderSummaryQuery(req.OrderId);

        var orderSummary = await _sender.Send(query);

        if (orderSummary is null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            await SendOkAsync(orderSummary);
        }
    }
}