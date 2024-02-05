namespace Messaging.Application.Features.RMessages.Commands.DeleteMessage;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeleteMessageCommand : IRequest
{
	public virtual Guid Id { get; set; }
	public DeleteMessageCommand(Guid id)
	{
		Id = id;
	}
}


