using Moq;
using zOrdo.Models.Enums;
using zOrdo.Models.Models;
using zOrdo.Repositories.TodoItemRepository;
using zOrdo.Services.TaskSchedulingService;

namespace zOrdo.Tests.Services;

public class TaskSchedulingServiceTests
{
    [Fact]
    public async Task GeneratePlanAsync_Should_Order_By_DueDate_Then_Priority()
    {
        var mockRepository = new Mock<ITodoItemRepository>();
        var now = DateTime.UtcNow;

        var tasks = new List<TodoItem>
        {
            new()
            {
                Id = 1,
                Title = "Tomorrow Low",
                DueDateUtc = now.AddDays(1),
                Priority = Priority.Low,
                Status = Status.Pending
            },
            new()
            {
                Id = 2,
                Title = "Today High",
                DueDateUtc = now,
                Priority = Priority.High,
                Status = Status.Pending
            },
            new()
            {
                Id = 3,
                Title = "Today Low",
                DueDateUtc = now,
                Priority = Priority.Low,
                Status = Status.Pending
            }
        };

        mockRepository
            .Setup(r => r.GetIncompleteTasksAsync(It.IsAny<int>()))
            .ReturnsAsync(tasks);

        var service = new TaskSchedulingService(mockRepository.Object);

        var result = await service.GeneratePlanAsync(1);
        var ordered = result.Result;

        Assert.NotNull(ordered);
        Assert.Equal(3, ordered.Count);

        Assert.Equal("Today High", ordered[0].Title);
        Assert.Equal("Today Low", ordered[1].Title);
        Assert.Equal("Tomorrow Low", ordered[2].Title);
    }
    
    [Fact]
    public async Task GeneratePlanAsync_Should_Return_Empty_List_When_No_Tasks()
    {
        var mockRepository = new Mock<ITodoItemRepository>();

        mockRepository
            .Setup(r => r.GetIncompleteTasksAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<TodoItem>());

        var service = new TaskSchedulingService(mockRepository.Object);

        var result = await service.GeneratePlanAsync(1);
        var ordered = result.Result;

        Assert.NotNull(ordered);
        Assert.Empty(ordered);
    }

}