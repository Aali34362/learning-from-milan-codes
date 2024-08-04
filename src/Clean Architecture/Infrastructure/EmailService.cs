using Application.Abstractions;

namespace Infrastructure;

internal sealed class EmailService : IEmailService
{
    public Task SendOrderConfirmationAsync(Guid orderId)
    {
        return Task.Delay(1000);
    }
}