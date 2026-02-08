using Microsoft.Extensions.Logging;
using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Models.Responses;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Services.UserService;

public class UserService(
    ILoggerFactory loggerFactory,
    IUserRepository userRepository) : IUserService
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<UserService>();

    public async Task<ZordoResult<UserResponse>> CreateUserAsync(User user)
    {
        var existingUser = await userRepository.GetUserAsync(user.Email);
        if (existingUser != null)
            return new ZordoResult<UserResponse>().CreateConflict("User with the given email already exists.");
        _logger.LogInformation("No user found with the given email, proceeding to create a new user: {@User}", user);
        var createdUser = await userRepository.CreateUserAsync(user);
        _logger.LogInformation("Created new user: {@User}", createdUser);
        return createdUser != null
            ? new ZordoResult<UserResponse>().CreateSuccess(new UserResponse().FromUserModel(createdUser))
            : new ZordoResult<UserResponse>().CreateConflict("Failed to create user.");
    }

    public async Task<ZordoResult<Paginated<UserResponse>>> GetUsersAsync(int pageNumber, int pageSize)
    {
        var paginatedUsers = await userRepository.GetUsersAsync(pageNumber, pageSize);
        var paginatedResult = new Paginated<UserResponse>
        {
            Items = paginatedUsers.Items.Select(user => new UserResponse().FromUserModel(user)).ToList(),
            TotalCount = paginatedUsers.TotalCount,
            PageNumber = paginatedUsers.PageNumber,
            PageSize = paginatedUsers.PageSize
        };
        return new ZordoResult<Paginated<UserResponse>>().CreateSuccess(paginatedResult);
    }

    public async Task<ZordoResult<UserResponse>> GetUserAsync(int id)
    {
        var user = await userRepository.GetUserAsync(id);
        return user != null
            ? new ZordoResult<UserResponse>().CreateSuccess(new UserResponse().FromUserModel(user))
            : new ZordoResult<UserResponse>().CreateConflict("User not found.");
    }

    public async Task<ZordoResult<UserResponse>> GetUserAsync(string email)
    {
        var user = await userRepository.GetUserAsync(email);
        return user != null
            ? new ZordoResult<UserResponse>().CreateSuccess(new UserResponse().FromUserModel(user))
            : new ZordoResult<UserResponse>().CreateNotFound("User not found.");
    }

    public async Task<ZordoResult<UserResponse>> UpdateUserAsync(User userRequest, string email)
    {
        var existingUser = await userRepository.GetUserAsync(email);
        if (existingUser == null) return new ZordoResult<UserResponse>().CreateNotFound("Requested user not found.");

        var request = new User
        {
            FirstName = userRequest.FirstName ?? existingUser.FirstName,
            LastName = userRequest.LastName ?? existingUser.LastName,
            Email = userRequest.Email ?? existingUser.Email,
        };

        var updatedUser = await userRepository.UpdateUserAsync(request, existingUser.Id);
        return updatedUser != null
            ? new ZordoResult<UserResponse>().CreateSuccess(new UserResponse().FromUserModel(updatedUser))
            : new ZordoResult<UserResponse>().CreateConflict("Failed to create user.");
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