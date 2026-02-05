using zOrdo.Models.Models;

namespace zOrdo.Services.UserService;

public interface IUserService
{
    public Task<ZordoResult<User>> CreateUserAsync(User user);
    public Task<ZordoResult<User>> GetUserAsync(int id);
    public Task<ZordoResult<User>> GetUserByEmailAsync(string email);
    public Task<ZordoResult<User>> UpdateUserAsync(User userRequest, string email);
    public Task<ZordoResult<bool>> DeleteUserAsync(string email);

    // public Task<List<User>> GetAllUsersAsync();
}