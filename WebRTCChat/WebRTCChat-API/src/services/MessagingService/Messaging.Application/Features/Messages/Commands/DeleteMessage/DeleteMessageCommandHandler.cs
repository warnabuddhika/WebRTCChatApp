namespace Messaging.Application.Features.Messages.Commands.DeleteMessage;
using Common.Domain.Exceptions;
using MediatR;
using Messaging.Application.Features.RMessages.Commands.DeleteMessage;
using Messaging.Domain.Entities;
using System.Threading.Tasks;

internal class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
{
	private readonly IMessageRepository _messageRepository;

	public DeleteMessageCommandHandler(IMessageRepository messageRepository)
	{
		_messageRepository = messageRepository;
	}

	public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
	{
		var room = await _messageRepository.GetAsync(e => e.Id == request.Id, cancellationToken);

		if (room == null)
		{
			throw new EntityNotFoundException(typeof(Room), request.Id);
		}
		await _messageRepository.RemoveAsync(room, cancellationToken);
		await _messageRepository.UnitOfWork.CommitChangesAsync(cancellationToken);
		return;
	}
}