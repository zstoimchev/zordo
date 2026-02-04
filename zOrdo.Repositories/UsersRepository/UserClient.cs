using System.Net.Http.Json;
using zOrdo.Models.Models;

namespace zOrdo.Repositories.UsersRepository;

public class UserClient(HttpClient client) : IUserRepository
{
    private const string RequestUri = "api/users";

    public async Task<User?> CreateUserAsync(User user)
    {
        var response = await client.PostAsJsonAsync(RequestUri, user);
        if (!response.IsSuccessStatusCode)
        {
            // TODO: handle logging and error handling
            return null;
        }

        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<User?> GetUserAsync(long id)
    {
        var response = await client.GetAsync($"{RequestUri}/{id}");
        response.EnsureSuccessStatusCode(); // todo: handle errors
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var response = await client.GetAsync($"{RequestUri}/{email}");
        response.EnsureSuccessStatusCode(); // todo: handle errors
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<User?> UpdateUserAsync(User user, int id)
    {
        var response = await client.PutAsJsonAsync($"{RequestUri}/{id}", user);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var response = await client.DeleteAsync($"{RequestUri}/{id}");
        return response.IsSuccessStatusCode;
    }
}