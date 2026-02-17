using zOrdo.Models.Models;
using zOrdo.Services.TaskSchedulingService;

namespace zOrdo.Services.StudyPlannerService;

public class StudyPlannerService(ITaskSchedulingStrategy strategy)
{
    public Task<List<TodoItem>> GetPlanAsync(int userId)
    {
        return strategy.GeneratePlanAsync(userId);
    }
}
