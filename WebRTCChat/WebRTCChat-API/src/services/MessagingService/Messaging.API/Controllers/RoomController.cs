namespace Messaging.API.Controllers;

using AutoMapper;
using MediatR;
using Messaging.API.Extensions;
using Messaging.API.Helpers;
using Messaging.Application.Features.Rooms.Commands.CreateRoom;
using Messaging.Application.Features.Rooms.Commands.RemoveRoomConnection;
using Messaging.Application.Features.Rooms.Commands.UpdateCountMember;
using Messaging.Application.Features.Rooms.Queries.GetAllRooms;
using Messaging.Application.Features.Rooms.Queries.GetRoomByConnectionId;
using Messaging.Application.Features.Rooms.Queries.GetRoomById;
using Messaging.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

public class RoomController : BaseApiController
{
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;

	public RoomController(IMapper mapper, IMediator mediator)
	{

		_mapper = mapper;
		this._mediator = mediator;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<RoomDto>>> GetAllRooms([FromQuery] RoomParams roomParams, CancellationToken cancellationToken)
	{


		var query = new GetAllRoomsQuery();
		var response = await _mediator.Send(query, cancellationToken);
		Response.AddPaginationHeader(response.CurrentPage, response.PageSize, response.TotalCount, response.TotalPages);

		return Ok(response);


	}

	[HttpPost]
	public async Task AddRoom(string name, CancellationToken cancellationToken)
	{

		var command = new CreateRoomCommand
		{
			RoomName = name,
			UserId = Guid.NewGuid()
		};
		await _mediator.Send(command, cancellationToken);

	}


	[HttpGet]
	public async Task<ActionResult<RoomDto>> GetRoomById([FromQuery] Guid roomId, CancellationToken cancellationToken)
	{

		var query = new GetRoomByIdQuery { RoomId = roomId };
		var response = await _mediator.Send(query, cancellationToken);

		return Ok(response);

	}


	[HttpGet]
	public async Task<ActionResult<RoomDto>> GetRoomByConnectionId([FromQuery] string connectionNumber, CancellationToken cancellationToken)
	{

		var query = new GetRoomByConnectionIdQuery { ConnectionId = connectionNumber };
		var response = await _mediator.Send(query, cancellationToken);

		return Ok(response);

	}

	[HttpPut]
	public async Task UpdateCountMember(UpdateCountMemberCommand command, CancellationToken cancellationToken)
	{
		
		await _mediator.Send(command, cancellationToken);		

	}

	[HttpPut]
	public async Task AddRoomConnection(AddRoomConnectionCommand command, CancellationToken cancellationToken)
	{

		await _mediator.Send(command, cancellationToken);
	}

	[HttpPut]
	public async Task RemoveRoomConnection(RemoveRoomConnectionCommand command, CancellationToken cancellationToken)
	{

		await _mediator.Send(command, cancellationToken);
	}

	
}
