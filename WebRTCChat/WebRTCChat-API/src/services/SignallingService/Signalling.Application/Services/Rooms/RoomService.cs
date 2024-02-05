namespace Signalling.Application.Services.Rooms;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Signalling.Application.Dtos;
using Signalling.Application.IntermediateModel;
using Signalling.Application.IntermediateModel.Api;
using Signalling.Application.Services.Api;
using Signalling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RoomService : IRoomService
{
	private readonly IApiService _apiService;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public RoomService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
	{
		_apiService = apiService;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task AddRoomConnection(int roomId, Connection connection)
	{
		var command = new RoomConncation { RoomId = roomId, Connection = connection };
		string json = JsonConvert.SerializeObject(command);
		var apiUrl = string.Format("/api/room");
		await _apiService.PutAsync<bool>(apiUrl, json).ConfigureAwait(false);
	}

	public async Task<ResponseResult<RoomDto>> GetRoomByConnectionId(string connectionId)
	{
		var apiUrl = string.Format("/api/room/{0}", connectionId);
		var roomResult = await _apiService.GetAsync<RoomDto>(apiUrl);
		return roomResult;
	}

	public async Task<ResponseResult<RoomDto>> GetRoomById(int roomId)
	{
		var apiUrl = string.Format("/api/room/{0}", roomId);
		var roomResult = await _apiService.GetAsync<RoomDto>(apiUrl);
		return roomResult;
	}

	public async Task RemoveRoomConnection(int roomId, Connection connection)
	{
		var command = new RoomConncation { RoomId = roomId, Connection = connection };
		string json = JsonConvert.SerializeObject(command);
		var apiUrl = string.Format("/api/room");
		await _apiService.PutAsync<bool>(apiUrl, json).ConfigureAwait(false);
	}

	public async Task UpdateCountMember(int roomId, int count)
	{
		var command = new UpdateCountMember { RoomId = roomId, Count = count };
		string json = JsonConvert.SerializeObject(command);
		var apiUrl = string.Format("/api/room");
		await _apiService.PutAsync<bool>(apiUrl, json).ConfigureAwait(false);
	}
}

	
