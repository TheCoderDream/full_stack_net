using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Todo_App.Application.Tags.Commands.DeleteTag;
public record DeleteTagCommand(int Id) : IRequest;
