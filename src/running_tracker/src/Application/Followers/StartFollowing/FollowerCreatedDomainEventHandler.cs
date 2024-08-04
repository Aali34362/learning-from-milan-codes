using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Notifications;
using Application.Users;
using Domain.Followers;
using MediatR;

namespace Application.Followers.StartFollowing;

internal class FollowerCreatedDomainEventHandler
    : INotificationHandler<FollowerCreatedDomainEvent>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly INotificationService _notificationService;

    public FollowerCreatedDomainEventHandler(
        IDbConnectionFactory dbConnectionFactory,
        INotificationService notificationService)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _notificationService = notificationService;
    }

    public async Task Handle(FollowerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

        UserDto user = await UserQueries.GetByIdAsync(notification.UserId, connection);

        await _notificationService.SendAsync(
            notification.FollowedId,
            $"{user.Name} started following you!",
            cancellationToken);
    }
}
