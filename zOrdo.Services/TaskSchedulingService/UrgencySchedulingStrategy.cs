using zOrdo.Models.Models;
using zOrdo.Repositories.TodoItemRepository;

namespace zOrdo.Services.TaskSchedulingService;

public class UrgencySchedulingStrategy(ITodoItemRepository repository) : ITaskSchedulingStrategy
{
    public async Task<List<TodoItem>> GeneratePlanAsync(int userId)
    {
        var tasks = await repository.GetTodoItemsAsync(userId);

        return tasks.Items
            .OrderBy(t => t.DueDateUtc)
            .ThenByDescending(t => t.Priority)
            .ToList();
    }
}
