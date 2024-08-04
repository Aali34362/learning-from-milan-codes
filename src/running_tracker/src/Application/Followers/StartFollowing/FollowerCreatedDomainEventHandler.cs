using Application.Abstractions.Notifications;
using Domain.Followers;
using MediatR;

namespace Application.Followers.StartFollowing;

internal sealed class FollowerCreatedDomainEventHandler
    : INotificationHandler<FollowerCreatedDomainEvent>
{
    private readonly INotificationService _notificationService;

    public FollowerCreatedDomainEventHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(FollowerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _notificationService.SendAsync(
            notification.FollowedId,
            "You just got a new follower",
            cancellationToken);
    }
}
