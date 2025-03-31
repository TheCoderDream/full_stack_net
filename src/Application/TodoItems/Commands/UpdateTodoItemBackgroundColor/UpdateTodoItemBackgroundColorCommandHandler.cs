using MediatR;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItemBackgroundColor;

public class UpdateTodoItemBackgroundColorCommandHandler : IRequestHandler<UpdateTodoItemBackgroundColorCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemBackgroundColorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemBackgroundColorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(TodoItem), request.Id);

        entity.BackgroundColor = request.BackgroundColor;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
} 