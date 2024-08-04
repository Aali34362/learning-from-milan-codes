namespace Modules.Users.Infrastructure.Authorization.SameUser.Options;

/// <summary>
/// Represents the same user authorization options.
/// </summary>
internal sealed class SameUserAuthorizationOptions
{
    /// <summary>
    /// Gets the cache key prefix.
    /// </summary>
    public string CacheKeyPrefix { get; init; } = string.Empty;

    /// <summary>
    /// Gets the cache time in minutes.
    /// </summary>
    public int CacheTimeInMinutes { get; init; }
}
