using MediatR;
using Messaging.Application.Dtos;
using Messaging.Application.Features.Rooms.ViewModels;

namespace Messaging.Application.Features.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommand : IRequest<RoomViewModel>
{
	public int RoomId { get; set; }
	public string RoomName { get; set; }
	public string UserId { get; set; }
	public string UserName { get; set; }
	public string DisplayName { get; set; }
	public int CountMember { get; set; }


}
