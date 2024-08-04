namespace Application.Abstractions;

public interface IPaymentService
{
    Task TryCreatePaymentRequest(Guid orderId);
}