namespace Application.Abstractions.Notifications;

public interface INotificationService
{
    Task SendAsync(Guid userId, string message, CancellationToken cancellationToken = default);
}
