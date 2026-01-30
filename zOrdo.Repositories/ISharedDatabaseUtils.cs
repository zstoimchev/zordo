using System.Data;

namespace zOrdo.Repositories;

public interface ISharedDatabaseUtils
{
    public IDbConnection CreateConnection();
}