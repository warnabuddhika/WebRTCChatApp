namespace Messaging.Application.Features.Rooms.Commands.DeleteRoom;
using Common.Domain.Exceptions;
using MediatR;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
{
	private readonly IRoomRepository _roomRepository;

	public DeleteRoomCommandHandler(IRoomRepository roomRepository)
	{
		_roomRepository = roomRepository;
	}

	public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
	{
		var room = await _roomRepository.GetAsync(e => e.Id == request.Id, cancellationToken);

		if (room == null)
		{
			throw new EntityNotFoundException(typeof(Room), request.Id);
		}
		await _roomRepository.RemoveAsync(room, cancellationToken);
		await _roomRepository.UnitOfWork.CommitChangesAsync(cancellationToken);
		return;
	}
}