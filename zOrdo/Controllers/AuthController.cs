using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using zOrdo.Models.Models;
using zOrdo.Models.Requests;
using zOrdo.Services.UserService;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    IUserService userService, 
    IConfiguration config) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var userResult = await userService.GetUserAsync(request.Email);
        if (!userResult.IsSuccessful) return Unauthorized("Invalid credentials");

        var user = userResult.Result;

        var passwordMatches = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        if (!passwordMatches) return Unauthorized("Invalid credentials");

        // Generate JWT
        var token = GenerateJwtToken(user);

        return Ok(new
        {
            token,
            expiresIn = int.Parse(config["Jwt:AccessTokenExpirationMinutes"]) * 60
        });
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
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(config["Jwt:AccessTokenExpirationMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}