using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace Todo_App.Application.TodoItems.Queries;
public class GetTodoItemsByTagsQueryHandler : IRequestHandler<GetTodoItemsByTagQuery, List<TodoItemBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsByTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TodoItemBriefDto>> Handle(GetTodoItemsByTagQuery request, CancellationToken cancellationToken)
    {
        var tagIds = request.TagIds.Distinct().ToList();

        var todoItems = await _context.TodoItems
            .Where(t => t.Tags.Any(tag => tagIds.Contains(tag.Id)))
            .ProjectTo<TodoItemBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return todoItems;
    }
}

