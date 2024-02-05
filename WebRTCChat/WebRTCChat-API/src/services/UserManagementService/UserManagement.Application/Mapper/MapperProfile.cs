using AutoMapper;
using UserManagement.Application.Dtos;
using UserManagement.Domain.Dtos;
using UserManagement.Domain.Entities;
using UserManagement.Application.Features.Users.Commands.CreateUser;

namespace UserManagement.Application.Mapper;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<AppUser, MemberDto>();

		CreateMap<RegisterDto, AppUser>();

		CreateMap<CreateUserCommand, AppUser>();

		CreateMap<Room, RoomDto>();
			
	}
}
