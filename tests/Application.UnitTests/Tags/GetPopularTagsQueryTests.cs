using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.Tags.Queries;
using Todo_App.Domain.Entities;
using Xunit;

namespace Todo_App.Application.UnitTests.Tags;
public class GetPopularTagsQueryTests
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly GetPopularTagsQueryHandler _handler;

    public GetPopularTagsQueryTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _handler = new GetPopularTagsQueryHandler(_mockContext.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPopularTags_WhenTagsExist()
    {
        var tags = new List<Tag>
        {
            new Tag { Name = "Tag1", TodoItems = new List<TodoItem> { new TodoItem() } },
            new Tag { Name = "Tag2", TodoItems = new List<TodoItem> { new TodoItem(), new TodoItem() } },
        };

        _mockContext.Setup(c => c.Tags.OrderByDescending(t => t.TodoItems.Count).Take(5).ToListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(tags);

        var query = new GetPopularTagsQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Contains("Tag1", result);
        Assert.Contains("Tag2", result);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoTagsExist()
    {
        _mockContext.Setup(c => c.Tags.OrderByDescending(t => t.TodoItems.Count).Take(5).ToListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Tag>());

        var query = new GetPopularTagsQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.IsEmpty(result);
    }
}

