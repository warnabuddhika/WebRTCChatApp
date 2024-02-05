namespace Messaging.Application.Features.Rooms.Queries.GetAllRooms;

using MediatR;
using Messaging.Application.Features.Rooms.ViewModels;
using Messaging.Domain.Dtos;
using Messaging.Domain.Entities;
using Messaging.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetAllRoomsQuery :IRequest<PagedList<RoomDto>?>
{
	private const int MaxPageSize = 10;
	public int PageNumber { get; set; } = 1;

	private int _pageSize = 5;

	public int PageSize
	{
		get => _pageSize;
		set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
	}
}
