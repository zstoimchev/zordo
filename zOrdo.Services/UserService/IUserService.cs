using zOrdo.Models.Models;

namespace zOrdo.Services.UserService;

public interface IUserService
{
    public Task<User> CreateUserAsync(User user);
    public Task<User> GetUserAsync(long id);
    public Task<User> GetUserByEmailAsync(string email);
    public Task<User> UpdateUserAsync(User userRequest, string email);
    public Task<bool> DeleteUserAsync(string email);

    // public Task<List<User>> GetAllUsersAsync();
}