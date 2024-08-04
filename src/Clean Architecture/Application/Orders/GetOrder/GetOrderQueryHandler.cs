using Application.Abstractions.Data;
using Dapper;
using Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.GetOrder;

internal sealed class GetOrderQueryHandler :
    IRequestHandler<GetOrderQuery, OrderResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetOrderQueryHandler(IApplicationDbContext context, ISqlConnectionFactory sqlConnectionFactory)
    {
        _context = context;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.Create();

        Dictionary<Guid, OrderResponse> ordersDictionary = new();

        await connection
            .QueryAsync<OrderResponse, LineItemResponse, OrderResponse>(
                """
                SELECT
                    o.id AS Id,
                    o.customer_id AS CustomerId,
                    li.id AS LineItemId,
                    li.price_amount AS Price
                FROM orders o
                JOIN line_items li ON o.id = li.order_id
                WHERE o.id = @OrderId
                """,
                (order, lineItem) =>
                {
                    if (ordersDictionary.TryGetValue(order.Id, out var existingOrder))
                    {
                        order = existingOrder;
                    }
                    else
                    {
                        ordersDictionary.Add(order.Id, order);
                    }

                    order.LineItems.Add(lineItem);

                    return order;
                },
                new
                {
                    request.OrderId
                },
                splitOn: "LineItemId");

        var orderResponse = ordersDictionary[request.OrderId];

        if (orderResponse is null)
        {
            throw new OrderNotFoundException(new OrderId(request.OrderId));
        }

        return orderResponse;
    }
}
