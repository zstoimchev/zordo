using Microsoft.Extensions.Logging;
using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Models.Requests;
using zOrdo.Models.Responses;
using zOrdo.Repositories.TodoItemRepository;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Services.TodoItemService;

public class TodoItemService(
    ILoggerFactory loggerFactory,
    ITodoItemRepository todoItemRepository,
    IUserRepository userRepository) : ITodoItemService
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TodoItemService>();

    public async Task<ZordoResult<TodoItemResponse>> CreateTodoItemAsync(
        string userEmail,
        TodoItemRequest todoItemRequest)
    {
        var user = await userRepository.GetUserAsync(userEmail);
        if (user == null) return new ZordoResult<TodoItemResponse>().CreateConflict("User not found");
        var todoItem = new TodoItem().FromRequest(user.Id, todoItemRequest);
        var createdTodoItem = await todoItemRepository.CreateTodoItemAsync(user.Id, todoItem);
        return createdTodoItem != null
            ? new ZordoResult<TodoItemResponse>().CreateSuccess(new TodoItemResponse().FromModel(createdTodoItem))
            : new ZordoResult<TodoItemResponse>().CreateConflict("Failed to create todo item.");
    }

    public async Task<ZordoResult<Paginated<TodoItemResponse>>> GetTodoItemsAsync(
        string userEmail,
        int pageNumber,
        int pageSize)
    {
        var user = await userRepository.GetUserAsync(userEmail);
        if (user == null) return new ZordoResult<Paginated<TodoItemResponse>>().CreateConflict("User not found");
        var paginatedTodoItems = await todoItemRepository.GetTodoItemsAsync(user.Id, pageNumber, pageSize);
        var paginatedResult = new Paginated<TodoItemResponse>
        {
            Items = paginatedTodoItems.Items.Select(todoItem => new TodoItemResponse().FromModel(todoItem)).ToList(),
            TotalCount = paginatedTodoItems.TotalCount,
            PageNumber = paginatedTodoItems.PageNumber,
            PageSize = paginatedTodoItems.PageSize
        };
        return new ZordoResult<Paginated<TodoItemResponse>>().CreateSuccess(paginatedResult);
    }

    public async Task<ZordoResult<TodoItemResponse>> GetTodoItemAsync(string userEmail, int taskId)
    {
        var user = await userRepository.GetUserAsync(userEmail);
        if (user == null) return new ZordoResult<TodoItemResponse>().CreateConflict("User not found");
        var todoItemResponse = await todoItemRepository.GetTodoItemAsync(user.Id, taskId);
        if (todoItemResponse == null) return new ZordoResult<TodoItemResponse>().CreateNotFound("Could not find task.");
        var todoItem = new TodoItemResponse().FromModel(todoItemResponse);
        return new ZordoResult<TodoItemResponse>().CreateSuccess(todoItem);
    }

    public async Task<ZordoResult<TodoItemResponse>> UpdateTodoItemAsync(
        string userEmail, 
        TodoItemRequest todoItemRequest, 
        int taskId)
    {
        var user = await userRepository.GetUserAsync(userEmail);
        if (user == null) return new ZordoResult<TodoItemResponse>().CreateConflict("User not found");
       var item = await todoItemRepository.GetTodoItemAsync(user.Id, taskId);
        if (item == null) return new ZordoResult<TodoItemResponse>().CreateNotFound("Could not find task.");
        var request = new TodoItem()
        {
            Title = todoItemRequest.Title,
            Description = todoItemRequest.Description,
            Priority = todoItemRequest.Priority,
            DueDateUtc = todoItemRequest.DueDateUtc
        };
        var updated = await todoItemRepository.UpdateTodoItemAsync(user.Id, request);
        return updated == null 
            ? new ZordoResult<TodoItemResponse>().CreateConflict("Failed to update task.") 
            : new ZordoResult<TodoItemResponse>().CreateSuccess(new TodoItemResponse().FromModel(updated));
    }

    public Task<bool> DeleteTodoItemAsync(string userEmail, int taskId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CompleteTodoItemAsync(string userEmail, int taskId)
    {
        throw new NotImplementedException();
    }
}