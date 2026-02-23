using Microsoft.Extensions.Logging;
using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    // private readonly JwtConfiguration _jwtConfig;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository userRepository,
        // JwtConfiguration jwtConfig,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        // _jwtConfig = jwtConfig;
        _logger = logger;
    }

    public Task<ZordoResult<object>> LoginAsync(object request)
    {
        // 1. Get user by email
        // 2. Verify password
        // 3. Generate JWT to
        // 4. Generate refresh token (optional for now)
        throw new System.NotImplementedException();
    }
    

    private string GenerateJwtToken(User user)
    {
        throw new NotImplementedException();
    }

    private string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    private bool VerifyPassword(string password, string? storedHash)
    {
        return true;
    }

    public Task<ZordoResult<object>> RefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<ZordoResult<bool>> LogoutAsync(int userId)
    {
        throw new NotImplementedException();
    }
}