using Application.Abstractions.Notifications;

namespace Infrastructure.Notifications;

internal sealed class NotificationService : INotificationService
{
    public Task SendAsync(Guid userId, string message, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
