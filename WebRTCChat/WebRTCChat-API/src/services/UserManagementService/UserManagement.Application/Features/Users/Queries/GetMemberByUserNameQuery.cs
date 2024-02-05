namespace UserManagement.Application.Features.Users.Queries;

using MediatR;
using UserManagement.Domain.Dtos;

public class GetMemberByUserNameQuery : IRequest<MemberDto>
{
	public virtual string UserName { get; set; }

}

