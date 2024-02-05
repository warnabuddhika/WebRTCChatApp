using Users.Domain.Enums;
using Common.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;
using UserManagement.Application.Dtos;

namespace UserManagement.Application.Features.Users.Commands.LoginUser;

    public class LoginUserCommand : IRequest<UserDto>
    {
	[Required]
	public string UserName { get; set; }
	[Required]
	[StringLength(20, MinimumLength = 6)]
	public string Password { get; set; }
}
