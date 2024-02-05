namespace Messaging.Application.Features.Rooms.Queries.GetRoomByConnectionId;

using AutoMapper;
using Common.Domain.Exceptions;
using MediatR;
using Messaging.Domain.Dtos;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;
using System.Threading.Tasks;

internal class GetRoomByConnectionIdQueryHandler : IRequestHandler<GetRoomByConnectionIdQuery, RoomDto>
{
	private readonly IRoomRepository _roomRepository;
	private readonly IMapper _mapper;

	public GetRoomByConnectionIdQueryHandler(IRoomRepository roomRepository, IMapper mapper)
	{
		_roomRepository = roomRepository;
		_mapper = mapper;
	}

	public async Task<RoomDto> Handle(GetRoomByConnectionIdQuery request, CancellationToken cancellationToken)
	{		
		var room = await _roomRepository.GetRoomForConnection(request.ConnectionId);
		return room == null ? throw new EntityNotFoundException(typeof(Room)) : _mapper.Map<RoomDto>(room);
	}
}
