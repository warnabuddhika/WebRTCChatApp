namespace Messaging.Application.Features.Messages.Queries.GetMessageById;

using AutoMapper;
using MediatR;
using Messaging.Application.Features.Messages.ViewModels;
using Messaging.Domain.Entities;
using System.Threading.Tasks;

public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, MessageViewModel>
{
	private readonly IMessageRepository _MessageRepository;
	private readonly IMapper _mapper;

	public GetMessageByIdQueryHandler(IMessageRepository MessageRepository, IMapper mapper)
	{
		_MessageRepository = MessageRepository;
		_mapper = mapper;
	}

	public async Task<MessageViewModel> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
	{
		var Message = await _MessageRepository.GetAsync(e => e.Id.Equals(request.messageId), cancellationToken);
		return _mapper.Map<MessageViewModel>(Message);
	}
}