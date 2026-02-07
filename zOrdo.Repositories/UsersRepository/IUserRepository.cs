using zOrdo.Models.Models;

namespace zOrdo.Repositories.UsersRepository;

public interface IUserRepository
{
    public Task<User?> CreateUserAsync(User user);
    public Task<User?> GetUserAsync(int id);
    public Task<User?> GetUserAsync(string email);
    public Task<User?> UpdateUserAsync(User user, int id);
    public Task<bool> DeleteUserAsync(int id);
}