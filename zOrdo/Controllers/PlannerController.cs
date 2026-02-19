using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Responses;
using zOrdo.Services.TaskSchedulingService;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlannerController(ITaskSchedulingService schedulingService) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<List<TodoItemResponse>>> GetPlan()
    {
        var userId = GetUserId();
        var plan = await schedulingService.GeneratePlanAsync(userId);
        return MapToActionResult(plan);
    }
}