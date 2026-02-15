using zOrdo.Models;

namespace zOrdo.Services.AuthService;

public interface IAuthService
{
    Task<ZordoResult<object>> LoginAsync(object request);
    Task<ZordoResult<object>> RefreshTokenAsync(string refreshToken);
    Task<ZordoResult<bool>> LogoutAsync(int userId);
}