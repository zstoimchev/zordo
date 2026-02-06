using Microsoft.Extensions.Logging;
using zOrdo.Models.Models;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Services.UserService;

public class UserService(
    ILoggerFactory loggerFactory,
    IUserRepository userRepository) : IUserService
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<UserService>();
    
    public async Task<ZordoResult<User>> CreateUserAsync(User user)
    {
        _logger.LogInformation("Creating user.");
        var existingUser = await userRepository.GetUserAsync(user.Email);
        if (existingUser != null)
            return new ZordoResult<User>().CreateConflict("User with the given email already exists.");
        _logger.LogInformation("No user found with email {Email}, proceeding to create user.", user.Email);
        var createdUser = await userRepository.CreateUserAsync(user);
        // TODO: custom mapper, and map to customerResponseDto
        return createdUser != null
            ? new ZordoResult<User>().CreateSuccess(createdUser)
            : new ZordoResult<User>().CreateConflict("Failed to create user.");
    }

    public async Task<ZordoResult<User>> GetUserAsync(int id)
    {
        var result = await userRepository.GetUserAsync(id);
        return result != null
            ? new ZordoResult<User>().CreateSuccess(result)
            : new ZordoResult<User>().CreateNotFound("User not found.");
    }

    public async Task<ZordoResult<User>> GetUserByEmailAsync(string email)
    {
        var result = await userRepository.GetUserAsync(email);
        return result != null
            ? new ZordoResult<User>().CreateSuccess(result)
            : new ZordoResult<User>().CreateNotFound("User not found.");
    }

    public async Task<ZordoResult<User>> UpdateUserAsync(User userRequest, string email)
    {
        var existingUser = await userRepository.GetUserAsync(email);
        if (existingUser == null)
            return new ZordoResult<User>().CreateNotFound("Requested user not found.");

        var updatedUser = new User
        {
            FirstName = userRequest.FirstName ?? existingUser.FirstName,
            LastName = userRequest.LastName ?? existingUser.LastName,
            Email = userRequest.Email ?? existingUser.Email,
        };

        var result = await userRepository.UpdateUserAsync(updatedUser, existingUser.Id);
        return result != null
            ? new ZordoResult<User>().CreateSuccess(result)
            : new ZordoResult<User>().CreateConflict("Failed to update user.");
    }

    public async Task<ZordoResult<bool>> DeleteUserAsync(string email)
    {
        var user = await userRepository.GetUserAsync(email);
        if (user == null) return new ZordoResult<bool>().CreateNotFound("User not found.");

        var deleted = await userRepository.DeleteUserAsync(user.Id);
        return deleted
            ? new ZordoResult<bool>().CreateSuccess(true)
            : new ZordoResult<bool>().CreateConflict("Failed to delete user.");
    }
}