using zOrdo.Models.Models;

namespace zOrdo.Services.TaskSchedulingService;

public interface ITaskSchedulingStrategy
{
    Task<List<TodoItem>> GeneratePlanAsync(int userId);
}
