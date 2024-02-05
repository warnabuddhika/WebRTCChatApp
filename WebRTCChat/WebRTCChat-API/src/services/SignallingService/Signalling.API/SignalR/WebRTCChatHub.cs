namespace Signalling.API.SignalR;

using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Signalling.API.Extensions;
using Signalling.Application.Dtos;
using Signalling.Application.Services.Members;
using Signalling.Application.Services.Rooms;
using Signalling.Domain.Entities;

public class WebRTCChatHub : Hub
{
	IMapper _mapper;
	IHubContext<VitalityHub> _presenceHub;
	VitalityTracker _presenceTracker;
	IRoomService _roomService;
	IMemberService _memberService;
	ShareScreenTracker _shareScreenTracker;

	public WebRTCChatHub( ShareScreenTracker shareScreenTracker, VitalityTracker presenceTracker,
		IHubContext<VitalityHub> presenceHub, IRoomService roomService, IMemberService memberService, IMapper mapper)
	{
		_mapper = mapper;		
		_presenceTracker = presenceTracker;
		_presenceHub = presenceHub;
		_shareScreenTracker = shareScreenTracker;
		_roomService = roomService;
		_memberService = memberService;
	}

	public override async Task OnConnectedAsync()
	{
		var httpContext = Context.GetHttpContext();
		var roomId = httpContext.Request.Query["roomId"].ToString();
		var roomIdInt = int.Parse(roomId);
		var username = Context.User.GetUsername();

		await _presenceTracker.UserConnected(new UserConnectionInfo(username, roomIdInt), Context.ConnectionId);

		await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
		await AddConnectionToGroup(roomIdInt); 

		
		var oneUserOnline = await _memberService.GetMember(username);		
		await Clients.Group(roomId).SendAsync("UserOnlineInGroup", oneUserOnline);

		var currentUsers = await _presenceTracker.GetOnlineUsers(roomIdInt);
		_roomService.UpdateCountMember(roomIdInt, currentUsers.Length);
		

		var currentConnections = await _presenceTracker.GetConnectionsForUser(new UserConnectionInfo(username, roomIdInt));
		await _presenceHub.Clients.AllExcept(currentConnections).SendAsync("CountMemberInGroup",
			   new { roomId = roomIdInt, countMember = currentUsers.Length });

		
		var userIsSharing = await _shareScreenTracker.GetUserIsSharing(roomIdInt);
		if (userIsSharing != null)
		{
			var currentBeginConnectionsUser = await _presenceTracker.GetConnectionsForUser(userIsSharing);
			if (currentBeginConnectionsUser?.Count > 0)
			{
				await Clients.Clients(currentBeginConnectionsUser).SendAsync("OnShareScreenLastUser", new { usernameTo = username, isShare = true });
			}

			await Clients.Caller.SendAsync("OnUserIsSharing", userIsSharing.UserName);
		}
	}

	public override async Task OnDisconnectedAsync(Exception exception)
	{
		var username = Context.User.GetUsername();
		var room = await RemoveConnectionFromGroup();
		var isOffline = await _presenceTracker.UserDisconnected(new UserConnectionInfo(username, room.Id), Context.ConnectionId);

		await _shareScreenTracker.DisconnectedByUser(username, room.Id);
		if (isOffline)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, room.Id.ToString());
			var temp = await _memberService.GetMember(username);
			await Clients.Group(room.Id.ToString()).SendAsync("UserOfflineInGroup", temp);

			var currentUsers = await _presenceTracker.GetOnlineUsers(room.Id);

			_roomService.UpdateCountMember(room.Id, currentUsers.Length);
			

			await _presenceHub.Clients.All.SendAsync("CountMemberInGroup",
				   new { roomId = room.Id, countMember = currentUsers.Length });
		}
		await base.OnDisconnectedAsync(exception);
	}

	public async Task SendMessage(CreateMessageDto createMessageDto)
	{
		var userName = Context.User.GetUsername();
		var senderResult = await _memberService.GetMember(userName);
		var sender = senderResult.Result;

		var roomResult = await _roomService.GetRoomByConnectionId(Context.ConnectionId);
		var room = roomResult.Result;

		if (room != null)
		{
			var message = new MessageDto
			{
				SenderUsername = userName,
				SenderDisplayName = sender.DisplayName,
				Content = createMessageDto.Content,
				MessageSent = DateTime.Now
			};
		
			await Clients.Group(room.RoomId.ToString()).SendAsync("NewMessage", message);
		}
	}

	public async Task MuteMicro(bool muteMicro)
	{
		var roomResult = await _roomService.GetRoomByConnectionId(Context.ConnectionId);
		var room = roomResult.Result;
		if (room != null)
		{
			await Clients.Group(room.RoomId.ToString()).SendAsync("OnMuteMicro", new { username = Context.User.GetUsername(), mute = muteMicro });
		}
		else
		{
			throw new HubException("group == null");
		}
	}

	public async Task MuteCamera(bool muteCamera)
	{
		var roomResult = await _roomService.GetRoomByConnectionId(Context.ConnectionId);
		var room = roomResult.Result;
		if (room != null)
		{
			await Clients.Group(room.RoomId.ToString()).SendAsync("OnMuteCamera", new { username = Context.User.GetUsername(), mute = muteCamera });
		}
		else
		{
			throw new HubException("group == null");
		}
	}

	public async Task ShareScreen(int roomid, bool isShareScreen)
	{
		if (isShareScreen)
		{
			await _shareScreenTracker.UserConnectedToShareScreen(new UserConnectionInfo(Context.User.GetUsername(), roomid));
			await Clients.Group(roomid.ToString()).SendAsync("OnUserIsSharing", Context.User.GetUsername());
		}
		else
		{
			await _shareScreenTracker.UserDisconnectedShareScreen(new UserConnectionInfo(Context.User.GetUsername(), roomid));
		}
		await Clients.Group(roomid.ToString()).SendAsync("OnShareScreen", isShareScreen);
		
	}

	public async Task ShareScreenToUser(int roomid, string username, bool isShare)
	{
		var currentBeginConnectionsUser = await _presenceTracker.GetConnectionsForUser(new UserConnectionInfo(username, roomid));
		if (currentBeginConnectionsUser?.Count > 0)
		{
			await Clients.Clients(currentBeginConnectionsUser).SendAsync("OnShareScreen", isShare);
		}
	}

	private async Task<Room> RemoveConnectionFromGroup()
	{
		var roomResult = await _roomService.GetRoomByConnectionId(Context.ConnectionId);
		var room = roomResult.Result;
		var connection = room.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
		if (connection != null)
		{
			_roomService.RemoveRoomConnection(room.RoomId, connection);
		}

		return _mapper.Map<Room>(room);
	}

	private async Task<Room> AddConnectionToGroup(int roomId)
	{
		var roomResult = await _roomService.GetRoomById(roomId);
		var room = roomResult.Result;
		var connection = new Connection(Context.ConnectionId, Context.User.GetUsername());
		if (room != null)
		{
			room.Connections.Add(connection);
		}

		return _mapper.Map<Room>(room);
	}
}
