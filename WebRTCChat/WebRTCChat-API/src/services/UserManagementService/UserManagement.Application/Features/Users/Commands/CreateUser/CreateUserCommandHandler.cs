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

namespace UserManagement.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
	private readonly ITokenService _tokenService;
	private readonly IMapper _mapper;
	private readonly UserManager<AppUser> _userManager;


	public CreateUserCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
	{
		_tokenService = tokenService;
		_mapper = mapper;
		_userManager = userManager;
	}


	public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		if (await UserExists(request.UserName))
		{
			throw new EntityNotFoundException(typeof(AppUser), request.UserName);
		}

		var user = _mapper.Map<AppUser>(request);

		user.UserName = request.UserName.ToLower();

		var result = await _userManager.CreateAsync(user, request.Password);
	

		var roleResult = await _userManager.AddToRoleAsync(user, "Guest");

	
		var userDto = new UserDto
		{
			UserName = user.UserName,
			DisplayName = user.DisplayName,
			LastActive = user.LastActive,
			Token = await _tokenService.CreateTokenAsync(user),
			PhotoUrl = null
		};

		return userDto;
	}

	private async Task<bool> UserExists(string username)
	{
		return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
	}
}
