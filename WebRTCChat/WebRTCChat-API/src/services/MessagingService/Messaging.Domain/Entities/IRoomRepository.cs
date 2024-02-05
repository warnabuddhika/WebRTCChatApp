namespace Messaging.Domain.Interfaces;

using Common.Domain.Repositories;
using Messaging.Domain.Dtos;
using Messaging.Domain.Entities;
using Messaging.Domain.Helpers;
using System.Threading.Tasks;
using Connection = Domain.Entities.Connection;

public interface IRoomRepository : IRepository<Room>
{
	Task<Room> GetRoomById(int roomId);
	Task<Room> GetRoomForConnection(string connectionId);
	void RemoveConnection(Connection connection);
	void AddRoom(Room room);
	Task<Room> DeleteRoom(int id);
	Task<Room> EditRoom(int id, string newName);
	Task DeleteAllRoom();
	Task<PagedList<RoomDto>> GetAllRoomAsync(RoomParams roomParams);
	//Task<RoomDto> GetRoomDtoById(int roomId);
	Task UpdateCountMember(int roomId, int count);
}
