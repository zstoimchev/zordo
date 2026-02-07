using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Models;
using zOrdo.Models.Responses;
using zOrdo.Services.UserService;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ZordoBaseController
{
    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUserAsync([FromBody] User user)
    {
        var result = await userService.CreateUserAsync(user);
        return MapToActionResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUserByIdAsync(int id)
    {
        var result = await userService.GetUserAsync(id);
        return MapToActionResult(result);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserByEmailAsync(string email)
    {
        // return Ok("success");
        var result = await userService.GetUserAsync(email);
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