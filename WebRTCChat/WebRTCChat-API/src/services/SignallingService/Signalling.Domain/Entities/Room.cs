namespace Signalling.Domain.Entities;
using Common.Domain.Auditing;
using System;
using System.Collections.Generic;

public class Room : AuditableEntity<int>, ISoftDelete
{

	private Room( string roomName)		   
	{
		RoomName = roomName;
		
	}

	public string RoomName { get; set; }
	public int CountMember { get; set; }

	public RoomUser AppUser { get; set; }
	public Guid UserId { get; set; }
	public ICollection<Connection> Connections { get; set; } = new List<Connection>();

	public bool IsDeleted { get; private set; }

	public static Room Create(string roomName)		  
	{
		var room = new Room(roomName)
		{
			AppUser = new RoomUser(),
			Connections = new List<Connection>()
		};

		return room;
	}

	public void Update(string roomName)
	{

		RoomName = roomName;
			
	}
}

