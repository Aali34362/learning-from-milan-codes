using Application.Abstractions.Messaging;

namespace Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : ICachedQuery<UserResponse>
{
    public string CacheKey => $"users-by-id-{UserId}";

    public TimeSpan? Expiration => null;
}
