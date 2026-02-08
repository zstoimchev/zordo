using zOrdo.Models.Enums;

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
}