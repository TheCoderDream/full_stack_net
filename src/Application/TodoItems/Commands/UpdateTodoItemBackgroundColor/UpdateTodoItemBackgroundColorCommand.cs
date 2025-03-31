using MediatR;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItemBackgroundColor;

public record UpdateTodoItemBackgroundColorCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string? BackgroundColor { get; init; }
} 