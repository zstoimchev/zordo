using System.Data;
using Microsoft.Data.Sqlite;

namespace zOrdo.Repositories;

public class SharedDatabaseUtils(string connectionString) : ISharedDatabaseUtils
{
    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(connectionString);
    }
}