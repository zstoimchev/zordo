using Microsoft.AspNetCore.Mvc;
using zOrdo.Services.UserService;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }
}