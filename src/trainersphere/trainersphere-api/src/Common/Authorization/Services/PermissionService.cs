using Authorization.Contracts;
using Authorization.Options;
using MassTransit;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Authorization.Services;

/// <summary>
/// Represents the permission service.
/// </summary>
internal sealed class PermissionService : IPermissionService
{
    private readonly IRequestClient<IUserPermissionsRequest> _userPermissionsRequestClient;
    private readonly IMemoryCache _memoryCache;
    private readonly PermissionAuthorizationOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionService"/> class.
    /// </summary>
    /// <param name="userPermissionsRequestClient">The user permissions request client.</param>
    /// <param name="memoryCache">The memory cache.</param>
    /// <param name="options">The options.</param>
    public PermissionService(
        IRequestClient<IUserPermissionsRequest> userPermissionsRequestClient,
        IMemoryCache memoryCache,
        IOptions<PermissionAuthorizationOptions> options)
    {
        _userPermissionsRequestClient = userPermissionsRequestClient;
        _memoryCache = memoryCache;
        _options = options.Value;
    }

    /// <inheritdoc />
    public async Task<HashSet<string>> GetPermissionsAsync(string identityProviderId, CancellationToken cancellationToken = default) =>
        await _memoryCache.GetOrCreateAsync(
            CreateCacheKey(identityProviderId),
            async cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.CacheTimeInMinutes);

                return await GetPermissionsInternalAsync(identityProviderId, cancellationToken);
            });

    private string CreateCacheKey(string identityProviderId) => $"{_options.CacheKeyPrefix}{identityProviderId}";

    private async Task<HashSet<string>> GetPermissionsInternalAsync(string identityProviderId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(identityProviderId))
        {
            return new HashSet<string>();
        }

        var request = new UserPermissionsRequest
        {
            UserIdentityProviderId = identityProviderId
        };

        Response<IUserPermissionsResponse> response = await _userPermissionsRequestClient
            .GetResponse<IUserPermissionsResponse>(request, cancellationToken);

        return response.Message.Permissions;
    }

    private sealed class UserPermissionsRequest : IUserPermissionsRequest
    {
        public string UserIdentityProviderId { get; init; } = string.Empty;
    }
}
