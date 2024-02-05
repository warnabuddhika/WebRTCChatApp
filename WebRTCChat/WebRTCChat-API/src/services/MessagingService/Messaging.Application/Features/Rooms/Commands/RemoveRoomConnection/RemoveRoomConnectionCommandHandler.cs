namespace Messaging.Application.Features.Rooms.Commands.RemoveRoomConnection;

using Common.Domain.Exceptions;
using MediatR;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RemoveRoomConnectionCommandHandler : IRequestHandler<RemoveRoomConnectionCommand>
{
	private readonly IRoomRepository _roomRepository;

	public RemoveRoomConnectionCommandHandler(IRoomRepository roomRepository)
	{
		_roomRepository = roomRepository;
	}

	public async Task Handle(RemoveRoomConnectionCommand request, CancellationToken cancellationToken)
	{
		var room = await _roomRepository.GetAsync(e => e.Id == request.Id, cancellationToken);

		if (room == null)
		{
			throw new EntityNotFoundException(typeof(Room), request.Id);
		}
		room.Connections.Remove(request.Connection);

		await _roomRepository.UpdateAsync(room, cancellationToken);
		await _roomRepository.UnitOfWork.CommitChangesAsync(cancellationToken);
		return;
	}
}
