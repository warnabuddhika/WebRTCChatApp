using MediatR;
using Messaging.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Messaging.Application.Features.Messages.Commands.CreateMessage;

public class CreateMessageCommand : IRequest
    {
	public Guid RoomId { get; set; }
	public Guid SenderId { get; set; }
	public Guid ReciverId { get; set; }
	public string SenderDisplayName { get; set; }
	public string SenderUsername { get; set; }
	public string Content { get; set; }
}
