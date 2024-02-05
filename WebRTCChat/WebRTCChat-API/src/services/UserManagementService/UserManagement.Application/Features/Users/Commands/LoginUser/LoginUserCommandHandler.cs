using AutoMapper;
using Common.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Diagnostics.CodeAnalysis;
using UserManagement.Application.Dtos;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Users.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDto>
{
	private readonly ITokenService _tokenService;
	private readonly IMapper _mapper;
	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;


	public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService tokenService, IMapper mapper)
	{
		_tokenService = tokenService;
		_mapper = mapper;
		_userManager = userManager;
		_signInManager = signInManager;
	}


	public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _userManager.Users
				.SingleOrDefaultAsync(x => x.UserName == request.UserName.ToLower());

		var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
	

		var userDto = new UserDto
		{
			UserName = user.UserName,
			DisplayName = user.DisplayName,
			LastActive = user.LastActive,
			Token = await _tokenService.CreateTokenAsync(user),
			PhotoUrl = user.PhotoUrl
		};
		return userDto;
	}

	private async Task<bool> UserExists(string username)
	{
		return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
	}
}
