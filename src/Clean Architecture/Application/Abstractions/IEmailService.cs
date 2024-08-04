namespace Application.Abstractions;

public interface IEmailService
{
    Task SendOrderConfirmationAsync(Guid orderId);
}
