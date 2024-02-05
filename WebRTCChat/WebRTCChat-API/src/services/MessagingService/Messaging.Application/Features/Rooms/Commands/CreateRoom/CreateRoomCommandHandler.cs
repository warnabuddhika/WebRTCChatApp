using AutoMapper;
using MediatR;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;

namespace Messaging.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand>
{

	private readonly IMapper _mapper;
	private readonly IRoomRepository _roomRepository;


	public CreateRoomCommandHandler(IMapper mapper, IRoomRepository roomRepository)
	{

		_mapper = mapper;
		_roomRepository = roomRepository;
	}


	public async Task Handle(CreateRoomCommand request, CancellationToken cancellationToken)
	{
		var room = Room.Create(request.RoomName);

		_ = await _roomRepository.InsertAsync(room, cancellationToken);
		await _roomRepository.UnitOfWork.CommitChangesAsync(cancellationToken);
		
		return;
	}


}
