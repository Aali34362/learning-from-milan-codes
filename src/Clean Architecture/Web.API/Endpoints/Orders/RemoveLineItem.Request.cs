using FastEndpoints;

namespace Web.API.Endpoints.Orders;

public class RemoveLineItemRequest
{
    [BindFrom("id")]
    public Guid OrderId { get; init; }

    [BindFrom("lineItemId")]
    public Guid LineItemId { get; init; }
}