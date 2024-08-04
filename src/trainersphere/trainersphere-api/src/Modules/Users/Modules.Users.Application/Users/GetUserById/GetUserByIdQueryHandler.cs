using Application.Data;
using Application.Messaging;
using Modules.Users.Domain.Users;
using Shared.Results;

namespace Modules.Users.Application.Users.GetUserById;

/// <summary>
/// Represents the <see cref="GetUserByIdQuery"/> handler.
/// </summary>
internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="sqlQueryExecutor">The SQL connection factory.</param>
    public GetUserByIdQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    /// <inheritdoc />
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        await Result.Success(request)
            .Bind(async query => Result.Create(await GetUserByIdAsync(query)))
            .MapFailure(() => UserErrors.NotFound(new UserId(request.UserId)));

    private async Task<UserResponse?> GetUserByIdAsync(GetUserByIdQuery query) =>
        await _sqlQueryExecutor.FirstOrDefaultAsync<UserResponse>(
                @"SELECT id, email, first_name, last_name
                  FROM users.users
                  WHERE id = @UserId",
                new { query.UserId });
}
