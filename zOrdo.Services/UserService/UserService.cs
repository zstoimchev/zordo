using zOrdo.Models.Models;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Services.UserService;

public class UserService(
    IUserRepository userRepository
) : IUserService
{
    public async Task<ZordoResult<User>> CreateUserAsync(User user)
    {
        // check if user with the given email already exists. If yes, throw exception. If not, create user.
        var existingUser = await userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
            return new ZordoResult<User>().CreateConflict("User with the given email already exists.");

        var createdUser = await userRepository.CreateUserAsync(user);

        return createdUser == null
            ? new ZordoResult<User>().CreateConflict("Failed to create user.")
            : new ZordoResult<User>().CreateSuccess(createdUser);
    }

    public async Task<ZordoResult<User>> GetUserAsync(int id)
    {
        var result = await userRepository.GetUserAsync(id);
        return result == null
            ? new ZordoResult<User>().CreateNotFound("User with the given email does not exist.")
            : new ZordoResult<User>().CreateSuccess(result);
    }

    public async Task<ZordoResult<User>> GetUserByEmailAsync(string email)
    {
        var result = await userRepository.GetUserByEmailAsync(email);
        return result == null
            ? new ZordoResult<User>().CreateNotFound("User with the given email does not exist.")
            : new ZordoResult<User>().CreateSuccess(result);
    }

    public async Task<ZordoResult<User>> UpdateUserAsync(User userRequest, string email)
    {
        var existingUser = await userRepository.GetUserByEmailAsync(email);
        if (existingUser == null)
            return new ZordoResult<User>().CreateNotFound("User with the given email already exists.");

        var updatedUser = new User
        {
            FirstName = userRequest.FirstName ?? existingUser.FirstName,
            LastName = userRequest.LastName ?? existingUser.LastName,
            Email = userRequest.Email ?? existingUser.Email,
        };

        updatedUser = await userRepository.UpdateUserAsync(updatedUser, existingUser.Id);
        return new ZordoResult<User>().CreateSuccess(updatedUser);
    }

    public Task<bool> DeleteUserAsync(string email)
    {
        // first, get user by email. If exists, delete, it not, return false
        throw new NotImplementedException();
    }
}