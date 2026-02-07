using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using zOrdo.Models.Models;

namespace zOrdo.Repositories.UsersRepository;

public class UserClient(
    ILoggerFactory loggerFactory,
    HttpClient client) : IUserRepository
{
    private readonly ILogger<UserClient> _logger = loggerFactory.CreateLogger<UserClient>();
    private const string RequestUri = "api/users";

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<User?> CreateUserAsync(User user)
    {
        var response = await client.PostAsJsonAsync(RequestUri, user);
        var rawUser = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<User>(rawUser, _jsonOptions);
    }

    public async Task<User?> GetUserAsync(int id)
    {
        var response = await client.GetAsync($"{RequestUri}/{id}");
        var rawUser = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<User>(rawUser, _jsonOptions);
    }

    public async Task<User?> GetUserAsync(string email)
    {
        var response = await client.GetAsync($"{RequestUri}/{email}");
        var rawUser = await response.Content.ReadAsStringAsync();
        return !string.IsNullOrEmpty(rawUser)
            ? JsonSerializer.Deserialize<User>(rawUser, _jsonOptions)
            : null;
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