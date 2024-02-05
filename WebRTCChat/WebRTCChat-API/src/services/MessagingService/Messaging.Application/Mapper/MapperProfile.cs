using AutoMapper;
using Messaging.Application.Dtos;
using Messaging.Application.Features.Rooms.Queries.GetAllRooms;
using Messaging.Domain.Dtos;
using Messaging.Domain.Entities;
using Messaging.Domain.Helpers;

namespace Users.Application.Mapper;

public class MapperProfile : Profile
    {
	public MapperProfile()
	{


		CreateMap<Room, RoomDto>();
		CreateMap<RoomParams, GetAllRoomsQuery>().ReverseMap();
		//.ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
		//.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName));



	}
    }
