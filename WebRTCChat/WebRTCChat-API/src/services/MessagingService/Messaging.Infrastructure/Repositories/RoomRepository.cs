using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Domain.Repositories;
using Messaging.Domain.Dtos;
using Messaging.Domain.Entities;
using Messaging.Domain.Helpers;
using Messaging.Infrastructure;
using Messaging.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Connection = Messaging.Domain.Entities.Connection;
using Common.EntityFrameworkCore.Repositories;

namespace Users.Infrastructure.Repositories;

public class RoomRepository : Repository<Room>, IRoomRepository
{
	private readonly RoomDbContext _context;
	private readonly IMapper _mapper;

	public RoomRepository(RoomDbContext context, IMapper mapper) :base(context)
	{
		_context = context;
		_mapper = mapper;
	}
	protected override IQueryable<Room> IncludeAll(IQueryable<Room> query)
	{
		return query;
	}
	

	public void AddRoom(Room room)
	{
		_context.Rooms.Add(room);
	}

	public async Task DeleteAllRoom()
	{
		var list = await _context.Rooms.ToListAsync();
		_context.RemoveRange(list);
	}

	public async Task<Room> DeleteRoom(int id)
	{
		var room = await _context.Rooms.FindAsync(id);
		if (room != null)
		{
			_context.Rooms.Remove(room);
		}
		return room;
	}

	public async Task<Room> EditRoom(int id, string newName)
	{
		var room = await _context.Rooms.FindAsync(id);
		if (room != null)
		{
			room.RoomName = newName;
		}
		return room;
	}



	public  async Task<PagedList<RoomDto>> GetAllRoomAsync(RoomParams roomParams)
	{
		var list = _context.Rooms.AsQueryable();
		//using AutoMapper.QueryableExtensions; list.ProjectTo<RoomDto>
		return await PagedList<RoomDto>.CreateAsync(list.ProjectTo<RoomDto>(_mapper.ConfigurationProvider).AsNoTracking(), roomParams.PageNumber, roomParams.PageSize);
	}





	public Task<Room> GetRoomById(int roomId)
	{
		throw new NotImplementedException();
	}

	public  async Task<Room> GetRoomForConnection(string connectionId)
	{
		return await _context.Rooms.Include(x => x.Connections)
				.Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
				.FirstOrDefaultAsync();
	}



	public void RemoveConnection(Connection connection)
	{
		throw new NotImplementedException();
	}



	public async Task UpdateCountMember(int roomId, int count)
	{
		var room = await _context.Rooms.FindAsync(roomId);
		if (room != null)
		{
			room.CountMember = count;
		}
	}
}
