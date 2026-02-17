using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zOrdo.Models;
using zOrdo.Models.Requests;
using zOrdo.Models.Responses;
using zOrdo.Services.TodoItemService;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodoController(ITodoItemService todoItemService) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<TodoItemResponse>> CreateTodoItemAsync(
        [FromBody] TodoItemRequest todoItemRequest)
    {
        var userEmail = GetUserEmail();
        var result = await todoItemService.CreateTodoItemAsync(userEmail, todoItemRequest);
        return MapToActionResult(result);
    }

    [HttpGet]
    public async Task<ActionResult<Paginated<TodoItemResponse>>> GetTodoItemsAsync(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var userEmail = GetUserEmail();
        var result = await todoItemService.GetTodoItemsAsync(userEmail, pageNumber, pageSize);
        return MapToActionResult(result);
    }
    
    [HttpGet("{todoItemId:int}")]
    public async Task<ActionResult<TodoItemResponse>> GetTodoItemAsync(
        int todoItemId)
    {
        var userEmail = GetUserEmail();
        var result = await todoItemService.GetTodoItemAsync(userEmail, todoItemId);
        return MapToActionResult(result);
    }
}