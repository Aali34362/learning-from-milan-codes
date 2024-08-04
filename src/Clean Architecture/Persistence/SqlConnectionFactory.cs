using System.Data;
using Application.Abstractions.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistence;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Database") ??
                            throw new ApplicationException("Connection string is missing");
    }

    public IDbConnection Create()
    {
        return new NpgsqlConnection(_connectionString);
    }
}