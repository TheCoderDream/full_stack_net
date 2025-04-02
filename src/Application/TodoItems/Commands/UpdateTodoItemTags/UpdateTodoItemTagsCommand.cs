using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItemTags;
public record UpdateTodoItemTagsCommand : IRequest<Unit>
{
    public int TodoItemId { get; init; }
    public List<string> TagIds { get; init; } = new();
}
