namespace Application.Abstractions;

public interface IStockService
{
    Task ReserveForOrderAsync(Guid orderId);

    Task ReleaseForOrderAsync(Guid orderId);
}