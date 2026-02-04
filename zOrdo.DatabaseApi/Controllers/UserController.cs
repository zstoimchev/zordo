using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Models;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.DatabaseApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var created = await userRepository.CreateUserAsync(user);
        if (created is null) return BadRequest();

        return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<User>> GetUser(long id)
    {
        var user = await userRepository.GetUserAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUserByEmail([FromQuery] string email)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<User>> UpdateUser(int id, User user)
    {
        var updated = await userRepository.UpdateUserAsync(user, id);
        return updated == null ? Conflict() : Ok(user);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await userRepository.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}