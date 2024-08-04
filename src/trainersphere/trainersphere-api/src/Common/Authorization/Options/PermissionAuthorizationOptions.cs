namespace Authorization.Options;

/// <summary>
/// Represents the permission authorization options.
/// </summary>
internal sealed class PermissionAuthorizationOptions
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
