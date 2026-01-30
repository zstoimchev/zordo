using System.Data;

namespace zOrdo.Repositories;

public class SharedDatabaseUtils : ISharedDatabaseUtils
{
    public IDbConnection CreateConnection()
    {
        throw new NotImplementedException();
    }
}