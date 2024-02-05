namespace Rooms.API.Extensions;

using MediatR;
using Messaging.Application.Features.Rooms.Commands.CreateRoom;
using Messaging.Application.Features.Rooms.Commands.DeleteRoom;
using Messaging.Application.Features.Rooms.Commands.UpdateRoom;
using Messaging.Application.Features.Rooms.Queries.GetAllRooms;
using Messaging.Application.Features.Rooms.Queries.GetRoomById;
using Messaging.Application.Features.Rooms.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Users.API.Filter;

public static class RoomEndpoints
    {
	public static void MapRoomEndpoints(this IEndpointRouteBuilder app)
	{
		//#region Room
		//var room = app.MapGroup("api/room");
		//room.MapGet("{RoomId}", GetRoomById).RequireAuthorization();
		//room.MapGet("", GetRooms).RequireAuthorization();
		//room.MapPost("", CreateRoom).
		//	AddEndpointFilter<ValidationFilter<CreateRoomCommand>>();
		//room.MapPut("", UpdateRoom).RequireAuthorization();
		//room.MapDelete("{RoomId}", DeleteRoom).RequireAuthorization();
		//#endregion

	}

	//public static async Task CreateRoom(IMediator mediator, CreateRoomCommand command, CancellationToken cancellationToken)
	//{
	//	command.UserId = User.GetUserId();
	//	await mediator.Send(command, cancellationToken);
	//}

	//public static async Task<RoomViewModel> UpdateRoom(IMediator mediator, UpdateRoomCommand command, CancellationToken cancellationToken)
	//{
	//	var response = await mediator.Send(command, cancellationToken);
	//	return response;
	//}

	//public static async Task<Results<Ok<RoomViewModel>, NoContent>> GetRoomById(IMediator mediator, CancellationToken cancellationToken, Guid RoomId)
	//{
	//	var query = new GetRoomByIdQuery(RoomId);
	//	var response = await mediator.Send(query, cancellationToken);
	//	if (response == null)
	//	{
	//		return TypedResults.NoContent();
	//	}
	//	return TypedResults.Ok(response);
	//}

	//public static async Task DeleteRoom(IMediator mediator, Guid RoomId, CancellationToken cancellationToken)
	//{
	//	DeleteRoomCommand command = new DeleteRoomCommand(RoomId);
	//	await mediator.Send(command, cancellationToken);
	//}

	//public static async Task<Results<Ok<List<RoomViewModel>>, NoContent>> GetRooms(IMediator mediator, CancellationToken cancellationToken)
	//{
	//	var query = new GetAllRoomsQuery();
	//	var response = await mediator.Send(query, cancellationToken);
	//	if (response == null)
	//	{
	//		return TypedResults.NoContent();
	//	}
	//	return TypedResults.Ok(response);
	//}

}
