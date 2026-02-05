using zOrdo.Models.Models;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Services.UserService;

public class UserService(
    IUserRepository userRepository
) : IUserService
{
    public async Task<User?> CreateUserAsync(User user)
    {
        // check if user with the given email already exists. If yes, throw exception. If not, create user.
        var existingUser = await userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null) return null; // TODO: handle properly email already exists
        return await userRepository.CreateUserAsync(user);
    }

    public async Task<User?> GetUserAsync(int id)
    {
        var result = await userRepository.GetUserAsync(id);
        return result;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User?> UpdateUserAsync(User userRequest, string email)
    {
        var existingUser = await userRepository.GetUserByEmailAsync(email);
        if (existingUser == null) return null;

        var updatedUser = new User
        {
            FirstName = userRequest.FirstName ?? existingUser.FirstName,
            LastName = userRequest.LastName ?? existingUser.LastName,
            Email = userRequest.Email ?? existingUser.Email,
        };

        return await userRepository.UpdateUserAsync(updatedUser, existingUser.Id);
    }

    public Task<bool> DeleteUserAsync(string email)
    {
        // first, get user by email. If exists, delete, it not, return false
        throw new NotImplementedException();
    }
}