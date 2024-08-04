using Application.Abstractions.Messaging;

namespace Application.Followers.StartFollowing;

public sealed record StartFollowingCommand(Guid UserId, Guid FollowedId) : ICommand;
