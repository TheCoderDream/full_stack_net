using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;

namespace Todo_App.Application.Tags.Queries;
public record GetPopularTagsQuery : IRequest<List<string>>;

public class GetPopularTagsQueryHandler : IRequestHandler<GetPopularTagsQuery, List<string>>
{
    private readonly IApplicationDbContext _context;

    public GetPopularTagsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> Handle(GetPopularTagsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tags
            .OrderByDescending(t => t.TodoItems.Count)
            .Select(t => t.Name)
            .Take(5)
            .ToListAsync(cancellationToken);
    }
}

