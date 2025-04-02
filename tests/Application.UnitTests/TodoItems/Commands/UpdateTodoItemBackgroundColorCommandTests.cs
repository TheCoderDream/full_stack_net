using FluentAssertions;
using Moq;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.TodoItems.Commands.UpdateTodoItemBackgroundColor;
using Todo_App.Domain.Entities;
using Xunit;

namespace Todo_App.Application.UnitTests.TodoItems.Commands;
public class UpdateTodoItemBackgroundColorCommandTests
{
    private readonly Mock<IApplicationDbContext> _mockContext;

    public UpdateTodoItemBackgroundColorCommandTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_Should_Update_BackgroundColor()
    {
        var todoItem = new TodoItem { Id = 1, BackgroundColor = "#FFFFFF" };
        _mockContext.Setup(x => x.TodoItems.FindAsync(new object[] { 1 }, CancellationToken.None))
                    .ReturnsAsync(todoItem);

        var handler = new UpdateTodoItemBackgroundColorCommandHandler(_mockContext.Object);
        var command = new UpdateTodoItemBackgroundColorCommand { Id = 1, BackgroundColor = "#FF5733" };

        await handler.Handle(command, CancellationToken.None);

        todoItem.BackgroundColor.Should().Be("#FF5733");
        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
