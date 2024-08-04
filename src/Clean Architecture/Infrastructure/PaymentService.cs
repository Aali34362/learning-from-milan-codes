using Application.Abstractions;

namespace Infrastructure;

internal sealed class PaymentService : IPaymentService
{
    public async Task TryCreatePaymentRequest(Guid orderId)
    {
        await Task.Delay(1000);

        throw new Exception("Payment failed");
    }
}