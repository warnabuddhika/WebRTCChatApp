namespace Signalling.Application.Services.Rooms;

using Signalling.Application.Dtos;
using Signalling.Application.IntermediateModel.Api;
using Signalling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRoomService
{
	Task<ResponseResult<RoomDto>> GetRoomByConnectionId(string connectionId);
	Task<ResponseResult<RoomDto>> GetRoomById(int roomId);
	Task UpdateCountMember(int roomId, int count);
	Task AddRoomConnection(int roomId, Connection connection);
	Task RemoveRoomConnection(int roomId, Connection connection);

}
