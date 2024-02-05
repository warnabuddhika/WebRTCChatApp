namespace Messages.API.Extensions;

using MediatR;
using Messaging.Application.Features.Messages.Commands.CreateMessage;
using Messaging.Application.Features.Messages.Queries.GetAllMessages;
using Messaging.Application.Features.Messages.Queries.GetMessageById;
using Messaging.Application.Features.Messages.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Users.API.Filter;

public static class MessageEndpoints
{
	public static void MapMessageEndpoints(this IEndpointRouteBuilder app)
	{
		#region Message
		var Message = app.MapGroup("api/Messages");
		Message.MapGet("{MessageId}", GetMessageById).RequireAuthorization();
		Message.MapGet("", GetMessages).RequireAuthorization();
		Message.MapPost("", CreateMessage).
			AddEndpointFilter<ValidationFilter<CreateMessageCommand>>();
		#endregion

	}

	public static async Task CreateMessage(IMediator mediator, CreateMessageCommand command, CancellationToken cancellationToken)
	{
		await mediator.Send(command, cancellationToken);
	}



	public static async Task<Results<Ok<MessageViewModel>, NoContent>> GetMessageById(IMediator mediator, CancellationToken cancellationToken, Guid MessageId)
	{
		var query = new GetMessageByIdQuery(MessageId);
		var response = await mediator.Send(query, cancellationToken);
		if (response == null)
		{
			return TypedResults.NoContent();
		}
		return TypedResults.Ok(response);
	}



	public static async Task<Results<Ok<List<MessageViewModel>>, NoContent>> GetMessages(IMediator mediator, CancellationToken cancellationToken)
	{
		var query = new GetAllMessagesQuery();
		var response = await mediator.Send(query, cancellationToken);
		if (response == null)
		{
			return TypedResults.NoContent();
		}
		return TypedResults.Ok(response);
	}

}
