using zOrdo.Models;
using zOrdo.Models.Responses;
using zOrdo.Repositories.TodoItemRepository;

namespace zOrdo.Services.TaskSchedulingService;

public class TaskSchedulingService(
    ITodoItemRepository repository) : ITaskSchedulingService
{
    public async Task<ZordoResult<List<TodoItemResponse>>> GeneratePlanAsync(int userId)
    {
        var tasks = await repository.GetIncompleteTasksAsync(userId);

        var ordered = tasks
            .OrderBy(t => t.DueDateUtc)
            .ThenByDescending(t => t.Priority)
            .ToList();

        var result =  ordered
            .Select(t => new TodoItemResponse().FromModel(t))
            .ToList();
        
        return new ZordoResult<List<TodoItemResponse>>().CreateSuccess(result);
    }
}