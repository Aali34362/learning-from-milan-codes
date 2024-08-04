using FastEndpoints;

namespace Web.API.Endpoints.Orders;

public class GetOrderSummaryRequest
{
    [BindFrom("id")]
    public Guid OrderId { get; init; }
}