using zOrdo.Models.Enums;
using zOrdo.Models.Requests;

namespace zOrdo.Models.Models;

public class TodoItem : SharedDbProperties
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime DueDateUtc { get; set; }
    public DateTime CompletedOnUtc { get; set; }

    public TodoItem FromRequest(int userId, TodoItemRequest todoItemRequest)
    {
        UserId = userId;
        Title = todoItemRequest.Title;
        Description = todoItemRequest.Description;
        Priority = todoItemRequest.Priority;
        Status = Status.Pending;
        DueDateUtc = todoItemRequest.DueDateUtc;
        return this;
    }
}