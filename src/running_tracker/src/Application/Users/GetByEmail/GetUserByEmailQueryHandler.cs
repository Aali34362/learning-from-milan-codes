using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.Users;
using SharedKernel;

namespace Application.Users.GetById;

internal sealed class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetUserByEmailQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

        const string sql =
            """
            SELECT u.Id, u.Email, u.Name, u.HasPublicProfile
            FROM Users u
            WHERE u.Email = @Email
            """;

        UserResponse? user = await connection.QueryFirstOrDefaultAsync<UserResponse>(
            sql,
            new { query.Email });

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFoundByEmail(query.Email));
        }

        return user;
    }
}
