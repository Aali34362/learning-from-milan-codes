using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Notifications;
using Application.Users;
using Domain.Followers;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Followers.StartFollowing;

internal sealed class FollowerCreatedDomainEventHandler
    : INotificationHandler<FollowerCreatedDomainEvent>
{
    private readonly INotificationService _notificationService;
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public FollowerCreatedDomainEventHandler(
        INotificationService notificationService,
        IDbConnectionFactory dbConnectionFactory)
    {
        _notificationService = notificationService;
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task Handle(FollowerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

        UserDto user = await UserQueries.GetByIdAsync(notification.UserId, connection);

        await _notificationService.SendAsync(
            notification.FollowedId,
            $"{user.Name} started following you",
            cancellationToken);
    }
}
