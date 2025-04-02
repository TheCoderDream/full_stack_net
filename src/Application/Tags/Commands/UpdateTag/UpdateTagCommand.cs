using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Todo_App.Application.Tags.Commands.UpdateTag;
public record UpdateTagCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
