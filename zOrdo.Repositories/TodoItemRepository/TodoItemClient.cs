using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Repositories.TodoItemRepository;

public class TodoItemClient(
    ILoggerFactory loggerFactory,
    IHttpClientFactory httpClientFactory) : ITodoItemRepository
{
    private readonly ILogger<UserClient> _logger = loggerFactory.CreateLogger<UserClient>();
    private readonly HttpClient _client = httpClientFactory.CreateClient("zOrdo.DatabaseApi");
    private const string RequestUri = "api/todoItem";

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<TodoItem?> CreateTodoItemAsync(int userId, TodoItem todoItemRequest)
    {
        var response = await _client.PostAsJsonAsync($"{RequestUri}/{userId}", todoItemRequest);
        var rawResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TodoItem>(rawResponse, _jsonOptions);
    }

    public async Task<Paginated<TodoItem>> GetTodoItemsAsync(int userId, int pageNumber, int pageSize)
    {
        var requestUri = $"{RequestUri}/{userId}?pageNumber={pageNumber}&pageSize={pageSize}";
        var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var rawResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Paginated<TodoItem>>(rawResponse, _jsonOptions)!;
    }

    public async Task<TodoItem?> GetTodoItemAsync(int userId, int taskId)
    {
        var response = await _client.GetAsync($"{RequestUri}/{userId}/{taskId}");
        var rawResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TodoItem>(rawResponse, _jsonOptions);
    }

    public async Task<TodoItem?> UpdateTodoItemAsync(int userId, TodoItem todoItemRequest)
    {
        var requestUri = $"{RequestUri}/{userId}/{todoItemRequest.Id}";
        var response = await _client.PutAsJsonAsync(requestUri, todoItemRequest);
        // response.EnsureSuccessStatusCode();
        var rawResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TodoItem>(rawResponse, _jsonOptions);
    }

    public async Task<bool> DeleteTodoItemAsync(int userId, int taskId)
    {
        var response = await _client.DeleteAsync($"{RequestUri}/{userId}/{taskId}");
        return response.IsSuccessStatusCode;
    }

    public Task<bool> CompleteTodoItemAsync(int userId, int taskId)
    {
        throw new NotImplementedException();
    }
}