using Dapper;
using zOrdo.Models.Models;

namespace zOrdo.Repositories.UsersRepository;

public class UserRepository(
    ISharedDatabaseUtils utils
) : IUserRepository
{
    public Task<User> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserAsync(long id)
    {
        using var connection = utils.CreateConnection();
        const string sql = "SELECT * FROM Users WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
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