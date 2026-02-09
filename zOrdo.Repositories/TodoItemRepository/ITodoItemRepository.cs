using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Models.Requests;

namespace zOrdo.Repositories.TodoItemRepository;

public interface ITodoItemRepository
{
    Task<TodoItem?> CreateTodoItemAsync(int userId, TodoItem todoItemRequest);
    Task<Paginated<TodoItem>> GetTodoItemsAsync(int userId, int pageNumber, int pageSize);
    Task<TodoItem?> GetTodoItemAsync(int userId, int taskId);
    Task<TodoItem> UpdateTodoItemAsync(string userEmail, TodoItemRequest todoItemRequest, int taskId);
    Task<bool> DeleteTodoItemAsync(string userEmail, int taskId);
    Task<bool> CompleteTodoItemAsync(string userEmail, int taskId);
    
}