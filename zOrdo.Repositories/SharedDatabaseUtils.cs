using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using zOrdo.Models.Enums;

namespace zOrdo.Repositories;

public class SharedDatabaseUtils : ISharedDatabaseUtils
{
    private readonly DatabaseProvider _provider;
    private readonly string _connectionString;
    
    public SharedDatabaseUtils(IConfiguration configuration)
    {
        var dbSection = configuration.GetSection("Database");

        _provider = dbSection.GetValue<DatabaseProvider>("Provider");

        var template = dbSection.GetSection("ConnectionStrings").GetValue<string>(_provider.ToString());
        if (string.IsNullOrWhiteSpace(template)) throw new InvalidOperationException("Missing ConnectionString!");
        var password = dbSection["DefaultPassword"];

        _connectionString = template.Contains("{0}")
            ? string.Format(template, password)
            : template;
    }
    
    public IDbConnection CreateConnection()
    {
        return _provider switch
        {
            DatabaseProvider.SqLite => new SqliteConnection(_connectionString),
            DatabaseProvider.Postgres => new NpgsqlConnection(_connectionString),
            DatabaseProvider.Oracle => new OracleConnection(_connectionString),
            _ => throw new NotSupportedException($"Unsupported database: {_provider}")
        };
    }
}