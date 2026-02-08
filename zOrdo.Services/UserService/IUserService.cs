using zOrdo.Models;
using zOrdo.Models.Requests;
using zOrdo.Models.Responses;

namespace zOrdo.Services.UserService;

public interface IUserService
{
    public Task<ZordoResult<UserResponse>> CreateUserAsync(UserRequest user);
    public Task<ZordoResult<Paginated<UserResponse>>> GetUsersAsync(int pageNumber, int pageSize);
    public Task<ZordoResult<UserResponse>> GetUserAsync(int id);
    public Task<ZordoResult<UserResponse>> GetUserAsync(string email);
    public Task<ZordoResult<UserResponse>> UpdateUserAsync(UserRequest userRequest, string email);
    public Task<ZordoResult<bool>> DeleteUserAsync(string email);
}