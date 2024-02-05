namespace Messaging.Application.Features.Messages.Queries.GetAllMessages;

using AutoMapper;
using Common.Domain.Exceptions;
using MediatR;
using Messaging.Application.Features.Messages.ViewModels;
using Messaging.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, List<MessageViewModel>?>
{
	private readonly IMessageRepository _messageRepository;
	private readonly IMapper _mapper;

	public GetAllMessagesQueryHandler(IMessageRepository messageRepository, IMapper mapper)
	{
		_messageRepository = messageRepository;
		_mapper = mapper;
	}

	public async Task<List<MessageViewModel>?> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
	{

		var messages = await _messageRepository.GetAllAsync(null, cancellationToken);
		if (messages == null)
		{
			throw new EntityNotFoundException(typeof(Room));
		}
		var messageList = _mapper.Map<List<MessageViewModel>>(messages);
		return messageList;
	}
}