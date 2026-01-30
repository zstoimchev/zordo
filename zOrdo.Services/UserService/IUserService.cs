using zOrdo.Models.Models;

namespace zOrdo.Services.UserService;

public interface IUserService
{
    public Task<List<User>> GetAllUsersAsync();
}