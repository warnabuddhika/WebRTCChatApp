namespace Signalling.API.SignalR;

using Microsoft.AspNetCore.SignalR;
using Signalling.API.Extensions;
using Signalling.Application.Dtos;

public class VitalityHub : Hub
{
	private readonly VitalityTracker _tracker;
	public VitalityHub(VitalityTracker tracker)
	{
		_tracker = tracker;
	}
	public override async Task OnConnectedAsync()
	{
		var isOnline = await _tracker.UserConnected(new UserConnectionInfo(Context.User.GetUsername(), 0), Context.ConnectionId).ConfigureAwait(false);
	}

	public override async Task OnDisconnectedAsync(Exception exception)
	{
		var isOffline = await _tracker.UserDisconnected(new UserConnectionInfo(Context.User.GetUsername(), 0), Context.ConnectionId);
		await base.OnDisconnectedAsync(exception);
	}

}
