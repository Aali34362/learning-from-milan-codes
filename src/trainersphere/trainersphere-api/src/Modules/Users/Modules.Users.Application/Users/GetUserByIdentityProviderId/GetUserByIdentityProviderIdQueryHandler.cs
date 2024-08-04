using Application.Data;
using Application.Messaging;
using Modules.Users.Domain.Users;
using Shared.Results;

namespace Modules.Users.Application.Users.GetUserByIdentityProviderId;

/// <summary>
/// Represents the <see cref="GetUserByIdentityProviderIdQuery"/> handler.
/// </summary>
internal sealed class GetUserByIdentityProviderIdQueryHandler : IQueryHandler<GetUserByIdentityProviderIdQuery, UserResponse>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdentityProviderIdQueryHandler"/> class.
    /// </summary>
    /// <param name="sqlQueryExecutor">The SQL connection factory.</param>
    public GetUserByIdentityProviderIdQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    /// <inheritdoc />
    public async Task<Result<UserResponse>> Handle(GetUserByIdentityProviderIdQuery request, CancellationToken cancellationToken) =>
        await Result.Success(request)
            .Bind(async query => Result.Create(await GetUserByIdentityProviderIdAsync(query)))
            .MapFailure(() => UserErrors.NotFoundByIdentity(request.IdentityProviderId));

    private async Task<UserResponse?> GetUserByIdentityProviderIdAsync(GetUserByIdentityProviderIdQuery query)
    {
        UserResponse? userResponse = null;

        await _sqlQueryExecutor.QueryAsync<UserResponse, string, UserResponse>(
            @"SELECT u.id, u.email, u.first_name, u.last_name, p.name AS permission
              FROM users.users u
              JOIN users.user_roles ur ON u.id = ur.user_id
              JOIN users.roles r ON r.id = ur.role_id
              JOIN users.role_permissions rp ON r.id = rp.role_id
              JOIN users.permissions p ON p.id = rp.permission_id
              WHERE u.identity_provider_id = @IdentityProviderId",
            (user, permission) =>
            {
                (userResponse ??= user).Permissions.Add(permission);

                return user;
            },
            new { query.IdentityProviderId },
            "permission");

        return userResponse;
    }
}
