using Application.Orders.AddLineItem;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints.Orders;

public class AddLineItemApiRequest
{
    [FromRoute(Name = "id")]
    public Guid OrderId { get; init; }

    [FromBody]
    public AddLineItemRequest Request { get; init; }
}