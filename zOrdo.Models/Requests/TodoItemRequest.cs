using zOrdo.Models.Enums;

namespace zOrdo.Models.Requests;

public class TodoItemRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime DueDateUtc { get; set; }
}