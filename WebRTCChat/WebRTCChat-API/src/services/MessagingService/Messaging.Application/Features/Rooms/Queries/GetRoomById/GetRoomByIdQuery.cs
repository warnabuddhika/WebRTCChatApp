namespace Messaging.Application.Features.Rooms.Queries.GetRoomById;

using MediatR;
using Messaging.Application.Dtos;
using Messaging.Application.Features.Rooms.ViewModels;
using Messaging.Domain.Dtos;
using Messaging.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetRoomByIdQuery : IRequest<RoomDto>
{
	public virtual Guid RoomId { get; set; }

}
