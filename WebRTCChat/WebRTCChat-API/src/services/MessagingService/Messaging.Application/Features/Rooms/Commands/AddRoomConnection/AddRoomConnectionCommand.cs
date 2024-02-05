namespace Messaging.Application.Features.Rooms.Commands.RemoveRoomConnection;

using MediatR;
using Messaging.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AddRoomConnectionCommand : IRequest
{
	public virtual int Id { get; set; }
	public Connection Connection { get; set; }	

}
