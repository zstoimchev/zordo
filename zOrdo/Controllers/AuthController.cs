using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using zOrdo.Models.Models;
using zOrdo.Models.Requests;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    IUserRepository userRepository,
    IConfiguration config) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await userRepository.GetUserAsync(request.Email);
        if (user == null) return Unauthorized("Invalid credentials");

        var passwordMatches = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!passwordMatches) return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(user);

        return Ok(new { token, expiresIn = config.GetValue<int>("Jwt:AccessTokenExpirationMinutes") * 60 });
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("userId", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: config.GetValue<string>("Jwt:Issuer"),
            audience: config.GetValue<string>("Jwt:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(config.GetValue<int>("Jwt:AccessTokenExpirationMinutes")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}