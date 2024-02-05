using AutoMapper;
using MediatR;
using Messaging.Domain.Entities;

namespace Messaging.Application.Features.Messages.Commands.CreateMessage;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand>
{

	private readonly IMapper _mapper;
	private readonly IMessageRepository _messageRepository;


	public CreateMessageCommandHandler(IMapper mapper, IMessageRepository messageRepository)
	{

		_mapper = mapper;
		_messageRepository = messageRepository;
	}


	public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
	{
		var room = Message.Create(request.RoomId,request.SenderId,request.ReciverId,request.Content,request.SenderDisplayName,request.SenderUsername);


		_ = await _messageRepository.InsertAsync(room, cancellationToken);

		await _messageRepository.UnitOfWork.CommitChangesAsync(cancellationToken);

		return;
	}


}
