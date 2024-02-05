using MediatR;
using Messaging.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Messaging.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommand : IRequest
{
	public string RoomName { get; set; }
	public Guid UserId { get; set; }

}
