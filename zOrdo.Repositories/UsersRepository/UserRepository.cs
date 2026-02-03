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
            middle_name = user.LastName,
            last_name = user.LastName,
            email = user.Email,
            inserted_on_utc = DateTime.UtcNow
        });

        user.Id = insertedId;
        return insertedId > 0 ? user : null;
    }

    public async Task<User?> GetUserAsync(long id)
    {
        using var connection = utils.CreateConnection();
        const string sql = "SELECT * FROM Users WHERE Id = @id";
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { id = id });
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        using var connection = utils.CreateConnection();
        const string sql = "SELECT * FROM Users WHERE EMAIL = @email";
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { email = email });
    }

    public Task<User> UpdateUserAsync(User user, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}