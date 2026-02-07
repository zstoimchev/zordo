using Dapper;
using zOrdo.Models.Models;

namespace zOrdo.Repositories.UsersRepository;

public class UserRepository(
    ISharedDatabaseUtils utils
) : IUserRepository
{
    public async Task<User?> CreateUserAsync(User user)
    {
        using var connection = utils.CreateConnection();
        const string sql = """
                           INSERT INTO Users (
                                              FIRST_NAME, 
                                              MIDDLE_NAME, 
                                              LAST_NAME, 
                                              EMAIL,
                                              INSERTED_ON_UTC
                           ) VALUES (
                                   @first_name, 
                                   @middle_name, 
                                   @last_name,
                                   @email,
                                   @inserted_on_utc
                           ) RETURNING ID
                           """;

        var insertedId = await connection.ExecuteScalarAsync<int>(sql, new
        {
            first_name = user.FirstName,
            middle_name = user.MiddleName,
            last_name = user.LastName,
            email = user.Email,
            inserted_on_utc = DateTime.UtcNow
        });

        user.Id = insertedId;
        return insertedId > 0 ? user : null;
    }

    public async Task<User?> GetUserAsync(int id)
    {
        using var connection = utils.CreateConnection();
        const string sql = """
                           SELECT 
                               ID               AS Id,
                               FIRST_NAME       AS FirstName,
                               MIDDLE_NAME      AS MiddleName,
                               LAST_NAME        AS LastName,
                               EMAIL            AS Email,
                               PASSWORD_HASH    AS PasswordHash,
                               INSERTED_ON_UTC  AS InsertedOnUtc,
                               UPDATED_ON_UTC   AS UpdatedOnUtc,
                               DELETED_ON_UTC   AS DeletedOnUtc,
                               DELETED_BY       AS DeletedBy
                           FROM Users WHERE Id = @id
                           """;
        return (await connection.QuerySingleOrDefaultAsync<User>(sql, new { id = id }))!;
    }

    public async Task<User?> GetUserAsync(string email)
    {
        using var connection = utils.CreateConnection();
        const string sql = """
                           SELECT
                               ID               AS Id,
                               FIRST_NAME       AS FirstName,
                               MIDDLE_NAME      AS MiddleName,
                               LAST_NAME        AS LastName,
                               EMAIL            AS Email,
                               PASSWORD_HASH    AS PasswordHash,
                               INSERTED_ON_UTC  AS InsertedOnUtc,
                               UPDATED_ON_UTC   AS UpdatedOnUtc,
                               DELETED_ON_UTC   AS DeletedOnUtc,
                               DELETED_BY       AS DeletedBy
                           FROM Users WHERE EMAIL = @email
                           """;
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { email = email });
    }

    public async Task<User?> UpdateUserAsync(User user, int id)
    {
        using var connection = utils.CreateConnection();
        const string sql = """
                           UPDATE Users SET 
                               FIRST_NAME = @first_name,
                               MIDDLE_NAME = @middle_name,
                               LAST_NAME = @last_name,
                               EMAIL = @email
                           WHERE ID = @id
                           """;

        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            first_name = user.FirstName,
            middle_name = user.LastName,
            last_name = user.LastName,
            email = user.Email,
            id = id
        });

        return rowsAffected != 0 ? user : null;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        using var connection = utils.CreateConnection();
        const string sql = "DELETE FROM Users WHERE Id = @id";
        var rowsAffected = await connection.ExecuteAsync(sql, new { id = id });
        return rowsAffected != 0;
    }
}