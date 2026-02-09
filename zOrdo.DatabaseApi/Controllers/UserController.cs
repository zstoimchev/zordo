using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Models;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.DatabaseApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<User>> CreateUserAsync(User user)
    {
        var created = await userRepository.CreateUserAsync(user);
        return created is null ? BadRequest() : Ok(created);
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUsersAsync([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 50)
    {
        var user = await userRepository.GetUsersAsync(pageNumber, pageSize);
        return Ok(user);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUserAsync(int id)
    {
        var user = await userRepository.GetUserAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<User>> GetUserAsync(string email)
    {
        var user = await userRepository.GetUserAsync(email);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<User>> UpdateUserAsync(int id, User user)
    {
        var updated = await userRepository.UpdateUserAsync(user, id);
        return updated == null ? Conflict() : Ok(user);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var deleted = await userRepository.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}