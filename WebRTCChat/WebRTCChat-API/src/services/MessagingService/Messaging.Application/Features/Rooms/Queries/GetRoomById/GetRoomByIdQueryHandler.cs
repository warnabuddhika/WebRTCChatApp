namespace Messaging.Application.Features.Rooms.Queries.GetRoomById;

using AutoMapper;
using MediatR;
using Messaging.Application.Dtos;
using Messaging.Application.Features.Rooms.ViewModels;
using Messaging.Domain.Dtos;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, RoomDto>
{
	private readonly IRoomRepository _roomRepository;
	private readonly IMapper _mapper;

	public GetRoomByIdQueryHandler(IRoomRepository roomRepository, IMapper mapper)
	{
		_roomRepository = roomRepository;
		_mapper = mapper;
	}

	public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
	{
		var room = await _roomRepository.GetAsync(e => e.Id.Equals(request.RoomId), cancellationToken);
		return _mapper.Map<RoomDto>(room);
	}
}