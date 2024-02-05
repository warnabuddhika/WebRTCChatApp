namespace Messaging.Application.Features.Rooms.Commands.UpdateCountMember;
using MediatR;

using Messaging.Application.Features.Rooms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UpdateCountMemberCommand : IRequest
{
	public int RoomId { get; set; }	
	public int Count { get; set; }


}
