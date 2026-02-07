using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Models.Responses;
using Task = System.Threading.Tasks.Task;

namespace zOrdo.Services.UserService;

public interface IUserService
{
    public Task<ZordoResult<UserResponse>> CreateUserAsync(User user);
    public Task<Paginated<User>> GetUsersAsync(int pageNumber = 0, int pageSize = 50);
    public Task<ZordoResult<User>> GetUserAsync(int id);
    public Task<ZordoResult<User>> GetUserAsync(string email);
    public Task<ZordoResult<User>> UpdateUserAsync(User userRequest, string email);
    public Task<ZordoResult<bool>> DeleteUserAsync(string email);
}