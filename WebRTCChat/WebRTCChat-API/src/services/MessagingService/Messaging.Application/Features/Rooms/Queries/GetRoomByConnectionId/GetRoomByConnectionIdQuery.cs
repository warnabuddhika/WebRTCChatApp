namespace Messaging.Application.Features.Rooms.Queries.GetRoomByConnectionId;

using MediatR;
using Messaging.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetRoomByConnectionIdQuery : IRequest<RoomDto>
{
	public virtual string ConnectionId { get; set; }

}

