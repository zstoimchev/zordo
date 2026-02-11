using zOrdo.Models;
using zOrdo.Models.Models;

namespace zOrdo.Repositories.TodoItemRepository;

public interface ITodoItemRepository
{
    Task<TodoItem?> CreateTodoItemAsync(int userId, TodoItem todoItemRequest);
    Task<Paginated<TodoItem>> GetTodoItemsAsync(int userId, int pageNumber, int pageSize);
    Task<TodoItem?> GetTodoItemAsync(int userId, int taskId);
    Task<TodoItem?> UpdateTodoItemAsync(int userId, TodoItem todoItem);
    Task<bool> DeleteTodoItemAsync(string userEmail, int taskId);
    Task<bool> CompleteTodoItemAsync(string userEmail, int taskId);
    
}