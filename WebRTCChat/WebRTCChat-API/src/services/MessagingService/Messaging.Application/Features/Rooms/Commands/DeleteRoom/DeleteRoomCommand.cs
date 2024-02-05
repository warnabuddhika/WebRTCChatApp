namespace Messaging.Application.Features.Rooms.Commands.DeleteRoom;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeleteRoomCommand : IRequest
{
	public virtual int Id { get; set; }

	public DeleteRoomCommand(int id)
	{
		Id = id;
	}
}


