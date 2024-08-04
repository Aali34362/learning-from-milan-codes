using System.Data;

namespace Application.Abstractions.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateOpenConnection();
}
