using Endpoints.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Infrastructure.Authorization.SameUser.Requirements;
using Modules.Users.Infrastructure.Authorization.SameUser.Services;

namespace Modules.Users.Infrastructure.Authorization.SameUser.AuthorizationHandlers;

/// <summary>
/// Represents the same user policy authorization handler.
/// </summary>
internal sealed class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirement>
{
    private const string UserIdResourceName = "userId";
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="SameUserAuthorizationHandler"/> class.
    /// </summary>
    /// <param name="serviceScopeFactory">The service scope factory.</param>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    public SameUserAuthorizationHandler(IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement)
    {
        if (!TryGetUserIdentifiers(out (Guid UserId, string IdentityProviderId) userIdentifiers))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IUserChecker userChecker = scope.ServiceProvider.GetService<IUserChecker>()!;

        if (await userChecker.ExistsAsync(userIdentifiers.UserId, userIdentifiers.IdentityProviderId))
        {
            context.Succeed(requirement);
        }
    }

    private bool TryGetUserIdentifiers(out (Guid UserId, string IdentityProviderId) userIdentifiers)
    {
        string? identityProviderId = _httpContextAccessor.HttpContext?.User.GetIdentityProviderId();

        Guid? userIdFromRoute = GetUserIdFromRoute();

        if (identityProviderId is null || userIdFromRoute is null)
        {
            userIdentifiers = (Guid.Empty, string.Empty);

            return false;
        }

        userIdentifiers = (userIdFromRoute.Value, identityProviderId);

        return true;
    }

    private Guid? GetUserIdFromRoute()
    {
        RouteValueDictionary? routeValueDictionary = _httpContextAccessor.HttpContext?.GetRouteData().Values;

        if (routeValueDictionary is null ||
            !routeValueDictionary.TryGetValue(UserIdResourceName, out object? userIdValue) ||
            !Guid.TryParse(userIdValue?.ToString(), out Guid userId))
        {
            return null;
        }

        return userId;
    }
}
