using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using zOrdo.Models;
using zOrdo.Models.Models;

namespace zOrdo.Repositories.UsersRepository;

public class UserClient(
    ILoggerFactory loggerFactory,
    IHttpClientFactory httpClientFactory) : IUserRepository
{
    private readonly ILogger<UserClient> _logger = loggerFactory.CreateLogger<UserClient>();
    private readonly HttpClient _client = httpClientFactory.CreateClient("zOrdo.DatabaseApi");
    private const string RequestUri = "api/users";

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<User?> CreateUserAsync(User user)
    {
        var response = await _client.PostAsJsonAsync(RequestUri, user);
        var rawUser = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<User>(rawUser, _jsonOptions);
    }

    public async Task<Paginated<User>> GetUsersAsync(int pageNumber, int pageSize)
    {
        var response = await _client.GetAsync($"{RequestUri}?pageNumber={pageNumber}&pageSize={pageSize}");
        response.EnsureSuccessStatusCode();
        var rawUser = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Paginated<User>>(rawUser, _jsonOptions)!;
    }
    
    public async Task<User?> GetUserAsync(int id)
    {
        var response = await _client.GetAsync($"{RequestUri}/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        var rawUser = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<User>(rawUser, _jsonOptions);
    }

    public async Task<User?> GetUserAsync(string email)
    {
        var response = await _client.GetAsync($"{RequestUri}/{email}");
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        var rawUser = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<User>(rawUser, _jsonOptions);
    }

    public async Task<User?> UpdateUserAsync(User user, int id)
    {
        var response = await _client.PutAsJsonAsync($"{RequestUri}/{id}", user);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var response = await _client.DeleteAsync($"{RequestUri}/{id}");
        return response.IsSuccessStatusCode;
    }
}