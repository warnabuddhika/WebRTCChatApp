namespace Messaging.Application.Features.Rooms.Commands.UpdateCountMember;

using AutoMapper;
using Common.Domain.Exceptions;
using MediatR;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UpdateCountMemberCommandHandler : IRequestHandler<UpdateCountMemberCommand>
{
	private readonly IRoomRepository _roomRepository;
	private readonly IMapper _mapper;

	public UpdateCountMemberCommandHandler(IRoomRepository roomRepository, IMapper mapper)
	{
		_roomRepository = roomRepository;
		_mapper = mapper;
	}



	public async Task Handle([NotNull] UpdateCountMemberCommand request, CancellationToken cancellationToken)
	{
		var room = await _roomRepository.GetAsync(e => e.Id == request.RoomId, cancellationToken) ?? throw new EntityNotFoundException(typeof(Room), request.RoomId);

		if (room == null)
		{
			throw new EntityNotFoundException(typeof(Room), request.RoomId);
		}
		room.CountMember = request.Count;

		await _roomRepository.UpdateAsync(room, cancellationToken);

		await _roomRepository.UnitOfWork.CommitChangesAsync(cancellationToken);

	}
}
