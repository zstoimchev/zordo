using zOrdo.Models;
using zOrdo.Models.Models;

namespace zOrdo.Repositories.TodoItemRepository;

public interface ITodoItemRepository
{
    Task<TodoItem?> CreateTodoItemAsync(int userId, TodoItem todoItemRequest);
    Task<Paginated<TodoItem>> GetTodoItemsAsync(int userId, int pageNumber = 1, int pageSize = 10);
    Task<TodoItem?> GetTodoItemAsync(int userId, int taskId);
    Task<TodoItem?> UpdateTodoItemAsync(int userId, TodoItem todoItem);
    Task<bool> DeleteTodoItemAsync(int userId, int taskId);
    Task<bool> CompleteTodoItemAsync(int userId, int taskId);
    Task<List<TodoItem>> GetIncompleteTasksAsync(int userId);
}