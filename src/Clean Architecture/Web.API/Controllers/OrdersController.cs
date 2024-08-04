using Application.Orders.AddLineItem;
using Application.Orders.Create;
using Application.Orders.GetOrderSummary;
using Application.Orders.RemoveLineItem;
using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateOrder(Guid customerId)
    //{
    //    var command = new CreateOrderCommand(customerId);

    //    await _sender.Send(command);

    //    return Ok();
    //}

    //[HttpPut("{id}/line-items")]
    //public async Task<IActionResult> AddLineItem(Guid id, [FromBody] AddLineItemRequest request)
    //{
    //    var command = new AddLineItemCommand(
    //        new OrderId(id),
    //        new ProductId(request.ProductId),
    //        request.Currency,
    //        request.Amount);

    //    await _sender.Send(command);

    //    return Ok();
    //}

    //[HttpDelete("{id}/line-items/{lineItemId}")]
    //public async Task<IActionResult> RemoveLineItem(Guid id, Guid lineItemId)
    //{
    //    var command = new RemoveLineItemCommand(new OrderId(id), new LineItemId(lineItemId));

    //    await _sender.Send(command);

    //    return Ok();
    //}

    //[HttpGet("{id}/summary")]
    //public async Task<IActionResult> GetSummary(Guid id)
    //{
    //    var query = new GetOrderSummaryQuery(id);

    //    return Ok(await _sender.Send(query));
    //}
}
