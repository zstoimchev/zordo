using Microsoft.AspNetCore.Mvc;
using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Models.Requests;
using zOrdo.Models.Responses;
using zOrdo.Services.UserService;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUserAsync([FromBody] UserRequest user)
    {
        var result = await userService.CreateUserAsync(user);
        return MapToActionResult(result);
    }

    [HttpGet]
    public async Task<ActionResult<Paginated<UserResponse>>> GetUsersAsync(
        [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10)
    {
        var result = await userService.GetUsersAsync(pageNumber, pageSize);
        return MapToActionResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponse>> GetUserByIdAsync(int id)
    {
        var result = await userService.GetUserAsync(id);
        return MapToActionResult(result);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<UserResponse>> GetUserByEmailAsync(string email)
    {
        var result = await userService.GetUserAsync(email);
        return MapToActionResult(result);
    }

    [HttpPut("{email}")]
    public async Task<ActionResult<UserResponse>> UpdateUserAsync([FromBody] User user, string email)
    {
        var result = await userService.UpdateUserAsync(user, email);
        return MapToActionResult(result);
    }

    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteUserAsync(string email)
    {
        var result = await userService.DeleteUserAsync(email);
        return Ok(result);
    }
}