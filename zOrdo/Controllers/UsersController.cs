using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Models;
using zOrdo.Services.UserService;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] User user)
    {
        var result = await userService.CreateUserAsync(user);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var result = await userService.GetUserAsync(id);
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserByEmailAsync(string email)
    {
        var result = await userService.GetUserByEmailAsync(email);
        return Ok(result);
    }

    [HttpPut("email")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] User user, string email)
    {
        var result = await userService.UpdateUserAsync(user, email);
        return Ok(result);
    }

    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteUserAsync(string email)
    {
        var result = await userService.DeleteUserAsync(email);
        return Ok(result);
    }
}