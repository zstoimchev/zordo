using Microsoft.AspNetCore.Mvc;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.DatabaseApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserRepository userRepository) : ControllerBase
{
}