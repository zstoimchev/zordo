using zOrdo.Models.Models;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Services.UserService;

public class UserService(
    IUserRepository userRepository
    ) : IUserService
{
    public Task<User> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserAsync(int id)
    {
        return await userRepository.GetUserAsync(id);
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User> UpdateUserAsync(User userRequest, string email)
    {
        var existingUser = await userRepository.GetUserByEmailAsync(email);
        if (existingUser == null) return null; // TODO: deal with not found

        var updatedUser = new User
        {
            FirstName = userRequest.FirstName ?? existingUser.FirstName,
            LastName = userRequest.LastName ?? existingUser.LastName,
            Email = userRequest.Email ?? existingUser.Email,
        };
        
        updatedUser = await userRepository.UpdateUserAsync(updatedUser, existingUser.Id);
        return updatedUser;
    }

    public Task<bool> DeleteUserAsync(string email)
    {
        // first, get user by email. If exists, delete, it not, return false
        throw new NotImplementedException();
    }
}