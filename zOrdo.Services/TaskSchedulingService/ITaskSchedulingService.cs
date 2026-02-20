using zOrdo.Models;
using zOrdo.Models.Responses;

namespace zOrdo.Services.TaskSchedulingService;

public interface ITaskSchedulingService
{
    Task<ZordoResult<List<TodoItemResponse>>> GeneratePlanAsync(int userId);
}