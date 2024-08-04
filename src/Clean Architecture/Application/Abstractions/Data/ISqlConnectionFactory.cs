using System.Data;

namespace Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}