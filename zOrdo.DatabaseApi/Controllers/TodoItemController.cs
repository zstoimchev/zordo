using Microsoft.AspNetCore.Mvc;
using zOrdo.Models.Models;
using zOrdo.Repositories.TodoItemRepository;

namespace zOrdo.DatabaseApi.Controllers;

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
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize)
    {
        var response = await todoItemRepository.GetTodoItemsAsync(userId, pageNumber, pageSize);
        return Ok(response);
    }
}