namespace Messaging.Application.Features.Rooms.Commands.UpdateRoom;
using AutoMapper;
using Common.Domain.Exceptions;

using MediatR;
using Messaging.Application.Features.Rooms.ViewModels;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, RoomViewModel>
{
	private readonly IRoomRepository _roomRepository;
	private readonly IMapper _mapper;

	public UpdateRoomCommandHandler(IRoomRepository roomRepository, IMapper mapper)
	{
		_roomRepository = roomRepository;
		_mapper = mapper;
	}



	public async Task<RoomViewModel> Handle([NotNull] UpdateRoomCommand request, CancellationToken cancellationToken)
	{
		var room = await _roomRepository.GetAsync(e => e.Id == request.RoomId, cancellationToken) ?? throw new EntityNotFoundException(typeof(Room), request.RoomId);

		if (room == null)
		{
			throw new EntityNotFoundException(typeof(Room), request.RoomId);
		}
		

		room.Update(request.RoomName);
			
		await _roomRepository.UpdateAsync(room, cancellationToken);

		await _roomRepository.UnitOfWork.CommitChangesAsync(cancellationToken);

		return _mapper.Map<RoomViewModel>(room);
	}
}

