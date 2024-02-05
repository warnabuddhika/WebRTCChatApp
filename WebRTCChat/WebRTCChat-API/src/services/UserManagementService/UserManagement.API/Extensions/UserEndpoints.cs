using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Dtos;
using UserManagement.Application.Features.Users.Queries;
using Users.API.Filter;
using UserManagement.Application.Features.Users.Commands.CreateUser;
using UserManagement.Application.Features.Users.Commands.LoginUser;

namespace Users.API.Extensions;

public static class UserEndpoints
{
	public static void MapUserEndpoints(this IEndpointRouteBuilder app)
	{
		#region User
		var account = app.MapGroup("api/Account");
		account.MapPost("register", CreateUser).
			AddEndpointFilter<ValidationFilter<CreateUserCommand>>();
		account.MapPost("login", LoginUser).
			AddEndpointFilter<ValidationFilter<LoginUserCommand>>();

		#endregion

		#region Member

		var member = app.MapGroup("api/member");
		member.MapGet("/{username}", GetMember)
			  .RequireAuthorization();

		#endregion

	}

	#region User
	public static async Task<IResult> CreateUser(IMediator mediator, CreateUserCommand command, CancellationToken cancellationToken)
	{
		var result = await mediator.Send(command, cancellationToken);
		return TypedResults.Ok(result);
	}

	public static async Task<IResult> LoginUser(IMediator mediator, LoginUserCommand command, CancellationToken cancellationToken)
	{
		var result = await mediator.Send(command, cancellationToken);
		return TypedResults.Ok(result);
	}

	#endregion

	#region Member
	public static async Task<IResult> GetMember(IMediator mediator, string  userName, CancellationToken cancellationToken)
	{
		var query = new GetMemberByUserNameQuery { UserName = userName };
		var member = await mediator.Send(query, cancellationToken);
		return member == null ? TypedResults.NoContent() : TypedResults.Ok(member);
	}

	#endregion
}
