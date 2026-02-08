using zOrdo.Models.Enums;
using zOrdo.Models.Models;

namespace zOrdo.Models.Responses;

public class TodoItemResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime InsertedOnUtc { get; set; }
    public DateTime DueDateUtc { get; set; }
    
    public TodoItemResponse FromModel(TodoItem createdTodoItem)
    {
        Title = createdTodoItem.Title;
        Description = createdTodoItem.Description;
        Status = createdTodoItem.Status;
        Priority = createdTodoItem.Priority;
        InsertedOnUtc = createdTodoItem.InsertedOnUtc;
        DueDateUtc = createdTodoItem.DueDateUtc;
        return this;
    }
}