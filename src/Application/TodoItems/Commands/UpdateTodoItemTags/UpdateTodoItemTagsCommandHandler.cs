using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItemTags;
public class UpdateTodoItemTagsCommandHandler : IRequestHandler<UpdateTodoItemTagsCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemTagsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemTagsCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await _context.TodoItems
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == request.TodoItemId, cancellationToken);

        if (todoItem == null)
            throw new NotFoundException(nameof(TodoItem), request.TodoItemId);
        var tagIds = request.TagIds.Select(int.Parse).ToList();
        var selectedTags = await _context.Tags
            .Where(t => tagIds.Contains(t.Id))
            .ToListAsync(cancellationToken);

        var newTags = selectedTags.Except(todoItem.Tags).ToList();

        var removedTags = todoItem.Tags.Except(selectedTags).ToList();

        foreach (var tag in newTags)
        {
            todoItem.Tags.Add(tag);
        }

        foreach (var tag in removedTags)
        {
            todoItem.Tags.Remove(tag);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}


