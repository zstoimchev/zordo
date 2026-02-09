using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Models;
using zOrdo.Services.AuthService;
using zOrdo.Services.UserService;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    IUserService userService, 
    IConfiguration config, 
    IAuthService authService) : BaseController
{
    private readonly IAuthService _authService = authService;
    private readonly IConfiguration _config = config;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var userModel = await userService.GetUserAsync(request.Email);
        var user = new User();
        
        if (user == null || !VerifyPassword(user, request.Password)) return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(user);
        
        return Ok(new 
        { 
            token = token,
            expiresIn = 3600
        });
    }

    private bool VerifyPassword(User user, string password)
    {
        return true;
    }

    private string GenerateJwtToken(User user)
    {
        throw new System.NotImplementedException();
    }
}