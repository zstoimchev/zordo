using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Models;
using zOrdo.Repositories.TodoItemRepository;

namespace zOrdo.DatabaseApi.Controllers;

[ApiController]
[Route("api/[controller]/{userId:int}")]
public class TodoItemController(ITodoItemRepository todoItemRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateTodoItemAsync(int userId, TodoItem todoItemRequest)
    {
        var response = await todoItemRepository.CreateTodoItemAsync(userId, todoItemRequest);
        return response is null ? BadRequest() : Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<TodoItem>> GetTodoItemsAsync(
        int userId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var response = await todoItemRepository.GetTodoItemsAsync(userId, pageNumber, pageSize);
        return Ok(response);
    }
}