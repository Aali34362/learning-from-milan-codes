using Application.ServiceLifetimes;
using FirebaseAdmin.Auth;
using Modules.Users.Domain.Users;
using Shared.Results;

namespace Modules.Users.Infrastructure.IdentityProvider;

/// <summary>
/// Represents the identity provider service.
/// </summary>
internal sealed class IdentityProviderService : IIdentityProviderService, ITransient
{
    public async Task<Result> ExistsAsync(string identityProviderId, CancellationToken cancellationToken = default) =>
        Result.Create(await FirebaseAuth.DefaultInstance.GetUserAsync(identityProviderId, cancellationToken) is not null);
}
