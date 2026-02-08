using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Models.Requests;
using zOrdo.Models.Responses;
using zOrdo.Repositories.TodoItemRepository;
using zOrdo.Repositories.UsersRepository;

namespace zOrdo.Services.TodoItemService;

public class TodoItemService(
    ITodoItemRepository todoItemRepository,
    IUserRepository userRepository) : ITodoItemService
{
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

    public Task<TodoItemResponse> GetTodoItemAsync(string userEmail, int taskId)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItemResponse> UpdateTodoItemAsync(string userEmail, TodoItemRequest todoItemRequest, int taskId)
    {
        throw new NotImplementedException();
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