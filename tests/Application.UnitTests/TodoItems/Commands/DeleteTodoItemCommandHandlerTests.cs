using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Todo_App.Application.TodoItems.Commands.DeleteTodoItem;
using Todo_App.Domain.Entities;
using Todo_App.Infrastructure.Persistence;
using Xunit;

namespace Todo_App.Application.UnitTests.TodoItems.Commands;
public class DeleteTodoItemCommandHandlerTests
{
    private readonly ApplicationDbContext _context;

    public DeleteTodoItemCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _context = new ApplicationDbContext(options, null!, null!, null!);
    }

    [Fact]
    public async Task Should_SoftDelete_TodoItem()
    {
        var todoItem = new TodoItem { Title = "Test Item" };
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        var command = new DeleteTodoItemCommand(todoItem.Id);
        var handler = new DeleteTodoItemCommandHandler(_context);

        await handler.Handle(command, CancellationToken.None);
        var deletedItem = await _context.TodoItems.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == todoItem.Id);

        Assert.NotNull(deletedItem);
        Assert.True(deletedItem.IsDeleted);
    }
}

