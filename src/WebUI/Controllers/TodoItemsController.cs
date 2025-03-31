using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.Common.Models;
using Todo_App.Application.Tags.Queries;
using Todo_App.Application.TodoItems.Commands.CreateTodoItem;
using Todo_App.Application.TodoItems.Commands.DeleteTodoItem;
using Todo_App.Application.TodoItems.Commands.UpdateTodoItem;
using Todo_App.Application.TodoItems.Commands.UpdateTodoItemBackgroundColor;
using Todo_App.Application.TodoItems.Commands.UpdateTodoItemDetail;
using Todo_App.Application.TodoItems.Commands.UpdateTodoItemTags;
using Todo_App.Application.TodoItems.Queries;
using Todo_App.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Todo_App.Domain.Entities;

namespace Todo_App.WebUI.Controllers;

public class TodoItemsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<TodoItemBriefDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTodoItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand(id));

        return NoContent();
    }
    [HttpPut("{id}/background")]
    public async Task<ActionResult> UpdateBackgroundColor(int id, UpdateTodoItemBackgroundColorCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
    [HttpPut("{id}/tags")]
    public async Task<ActionResult> UpdateTags(int id, UpdateTodoItemTagsCommand command)
    {
        if (id != command.TodoItemId)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
    //GET /api/todoitems/filter-by-tag?tagIds=1&tagIds=2&tagIds=3
    [HttpGet("filter-by-tag")]
    public async Task<ActionResult<List<TodoItemBriefDto>>> GetByTag([FromQuery] List<int> tagIds)
    {
        return await Mediator.Send(new GetTodoItemsByTagQuery { TagIds = tagIds });
    }

    [HttpGet("popular-tags")]
    public async Task<ActionResult<List<string>>> GetPopularTags()
    {
        return await Mediator.Send(new GetPopularTagsQuery());
    }
    [HttpGet("by-tags")]
    public async Task<ActionResult<List<TodoItemBriefDto>>> GetTodoItemsByTags([FromQuery] List<int> tagIds)
    {
        return await Mediator.Send(new GetTodoItemsByTagQuery { TagIds = tagIds });
    }
    [HttpGet("most-used-tags")]
    public async Task<ActionResult<List<string>>> GetMostUsedTags([FromQuery] int limit = 5)
    {
        return await Mediator.Send(new GetPopularTagsQuery());
    }
}
