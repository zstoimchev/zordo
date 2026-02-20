using Microsoft.AspNetCore.Mvc;
using zOrdo.Models;
using zOrdo.Models.Models;
using zOrdo.Repositories.TodoItemRepository;

namespace zOrdo.DatabaseApi.Controllers;

[ApiController]
[Route("api/[controller]/{userId:int}")]
public class TodoItemController(ITodoItemRepository todoItemRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateTodoItemAsync(
        int userId,
        TodoItem todoItemRequest)
    {
        var response = await todoItemRepository.CreateTodoItemAsync(userId, todoItemRequest);
        return response is null ? BadRequest() : Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<Paginated<TodoItem>>> GetTodoItemsAsync(
        int userId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var response = await todoItemRepository.GetTodoItemsAsync(userId, pageNumber, pageSize);
        return Ok(response);
    }

    [HttpGet("{todoItemId:int}")]
    public async Task<ActionResult<TodoItem>> GetTodoItemAsync(
        int userId,
        int todoItemId)
    {
        var response = await todoItemRepository.GetTodoItemAsync(userId, todoItemId);
        return response is null ? NotFound() : Ok(response);
    }

    [HttpPut("{todoItemId:int}")]
    public async Task<ActionResult<TodoItem>> UpdateTodoItemAsync(
        int userId,
        int todoItemId,
        TodoItem request)
    {
        var response = await todoItemRepository.UpdateTodoItemAsync(userId, request);
        return response is null ? NotFound() : Ok(response);
    }

    [HttpDelete("{todoItemId:int}")]
    public async Task<ActionResult> DeleteTodoItemAsync(
        int userId,
        int todoItemId)
    {
        var success = await todoItemRepository.DeleteTodoItemAsync(userId, todoItemId);
        return success ? NoContent() : NotFound();
    }

    [HttpPut("{todoItemId:int}/complete")]
    public async Task<ActionResult> CompleteTodoItemAsync(
        int userId,
        int todoItemId)
    {
        var success = await todoItemRepository.CompleteTodoItemAsync(userId, todoItemId);
        return success ? NoContent() : NotFound();
    }

    [HttpGet("incomplete")]
    public async Task<ActionResult> GetIncompleteTodoItemsAsync(
        int userId,
        int todoItemId)
    {
        var response = await todoItemRepository.GetIncompleteTasksAsync(userId);
        return Ok(response);
    }
}