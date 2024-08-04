using System.Data;
using Dapper;

namespace Application.Users;

internal static class UserQueries
{
    internal static async Task<UserDto> GetByIdAsync(Guid id, IDbConnection dbConnection)
    {
        return await dbConnection.QuerySingleAsync<UserDto>(
            """
            SELECT Id, Name
            FROM Users
            WHERE Id = @UserId
            """,
            new { UserId = id });
    }
}
