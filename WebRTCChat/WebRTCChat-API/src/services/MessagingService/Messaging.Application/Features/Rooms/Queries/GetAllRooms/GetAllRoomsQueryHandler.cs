namespace Messaging.Application.Features.Rooms.Queries.GetAllRooms;

using AutoMapper;
using Common.Domain.Exceptions;
using MediatR;
using Messaging.Application.Dtos;
using Messaging.Domain.Dtos;
using Messaging.Domain.Entities;
using Messaging.Domain.Helpers;
using Messaging.Domain.Interfaces;
using System.Threading.Tasks;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, PagedList<RoomDto>?>
{
	private readonly IRoomRepository _roomRepository;
	private readonly IMapper _mapper;

	public GetAllRoomsQueryHandler(IRoomRepository roomRepository, IMapper mapper)
	{
		_roomRepository = roomRepository;
		_mapper = mapper;
	}

	public async Task<PagedList<RoomDto>?> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
	{
		var roomParams = _mapper.Map<RoomParams>(request);
		var rooms = await _roomRepository.GetAllRoomAsync(roomParams);
		if (rooms == null)
		{
			throw new EntityNotFoundException(typeof(Room));
		}
		return rooms;	

	}
}