using zOrdo.Models;
using zOrdo.Models.Requests;
using zOrdo.Models.Responses;

namespace zOrdo.Services.TodoItemService;

public interface ITodoItemService
{
    Task<ZordoResult<TodoItemResponse>> CreateTodoItemAsync(string userEmail, TodoItemRequest todoItemRequest);
    Task<ZordoResult<Paginated<TodoItemResponse>>> GetTodoItemsAsync(string userEmail, int pageNumber, int pageSize);
    Task<ZordoResult<TodoItemResponse>> GetTodoItemAsync(string userEmail, int taskId);
    Task<TodoItemResponse> UpdateTodoItemAsync(string userEmail, TodoItemRequest todoItemRequest, int taskId);
    Task<bool> DeleteTodoItemAsync(string userEmail, int taskId);
    Task<bool> CompleteTodoItemAsync(string userEmail, int taskId);
}