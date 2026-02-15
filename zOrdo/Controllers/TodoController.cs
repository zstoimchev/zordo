using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zOrdo.Models;
using zOrdo.Models.Requests;
using zOrdo.Models.Responses;
using zOrdo.Services.TodoItemService;

namespace zOrdo.Controllers;

[ApiController]
[Route("api/[controller]/{userEmail}")]
[Authorize]
public class TodoController(ITodoItemService todoItemService) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<TodoItemResponse>> CreateTodoItemAsync(
        string userEmail,
        [FromBody] TodoItemRequest todoItemRequest)
    {
        var result = await todoItemService.CreateTodoItemAsync(userEmail, todoItemRequest);
        return MapToActionResult(result);
    }

    [HttpGet]
    public async Task<ActionResult<Paginated<TodoItemResponse>>> GetTodoItemsAsync(
        string userEmail,
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var result = await todoItemService.GetTodoItemsAsync(userEmail, pageNumber, pageSize);
        return MapToActionResult(result);
    }
    
    [HttpGet("{todoItemId:int}")]
    public async Task<ActionResult<TodoItemResponse>> GetTodoItemAsync(
        string userEmail,
        int todoItemId)
    {
        var result = await todoItemService.GetTodoItemAsync(userEmail, todoItemId);
        return MapToActionResult(result);
    }
}