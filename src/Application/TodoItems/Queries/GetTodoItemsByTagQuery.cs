using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace Todo_App.Application.TodoItems.Queries;
public record GetTodoItemsByTagQuery : IRequest<List<TodoItemBriefDto>>
{
    public List<int> TagIds { get; set; } = new();
}

