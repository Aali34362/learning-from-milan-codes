using Application.Abstractions;

namespace Infrastructure;

internal sealed class StockService : IStockService
{
    public Task ReserveForOrderAsync(Guid orderId)
    {
        return Task.Delay(1000);
    }

    public Task ReleaseForOrderAsync(Guid orderId)
    {
        return Task.Delay(1000);
    }
}