using System.Data;
using Dapper;

namespace Application.Users;

public static class UserQueries
{
    public static async Task<UserDto> GetByIdAsync(
        Guid id,
        IDbConnection connection)
    {
        return await connection.QuerySingleAsync<UserDto>(
            """
            SELECT Id, Name
            FROM Users
            WHERE Id = @UserId
            """,
            new { UserId = id });
    }
}
