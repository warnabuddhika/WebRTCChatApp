namespace Messaging.Application.Features.Messages.Queries.GetMessageById;

using MediatR;
using Messaging.Application.Dtos;
using Messaging.Application.Features.Messages.ViewModels;
using System;

public class GetMessageByIdQuery : IRequest<MessageViewModel>
{
	public readonly Guid messageId;
	public GetMessageByIdQuery(Guid messageId)
	{
		this.messageId = messageId;
	}
}
