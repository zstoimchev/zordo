using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Models;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.DatabaseApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserRepository userRepository, ILoggerFactory loggerFactory) : ControllerBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<UsersController>();

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var created = await userRepository.CreateUserAsync(user);
        if (created is null) return BadRequest();

        return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await userRepository.GetUserAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUserByEmail([FromQuery] string email)
    {
        _logger.LogInformation("this is a test");
        var user1 = new User()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "",
        };
        return Ok(user1);
        var user = await userRepository.GetUserAsync(email);
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