using zOrdo.Models.Enums;

namespace zOrdo.Models.Models;

public class Task : SharedProperties
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime DueDateUtc { get; set; }
    public DateTime CompletedOnUtc { get; set; }
    public TaskStatus Status { get; set; }
}