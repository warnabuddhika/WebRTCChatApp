namespace Messaging.Domain.Entities;

using Common.Domain.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

public class Message : AuditableEntity<Guid>, ISoftDelete
{
	private Message(Guid id, Guid roomId, Guid senderId, Guid reciverId , string content)
	   : base(id)
	{
		RoomId = roomId;
		SenderId = senderId;
		ReciverId = reciverId;
		Content = content;

	}
	public Guid RoomId { get; set; }
	public Guid SenderId { get; set; }
	public Guid ReciverId { get; set; }
	public string SenderDisplayName { get; set; }
	public string SenderUsername { get; set; }
	public string Content { get; set; }
	public DateTime MessageSent { get; set; }

	public bool IsDeleted { get; private set; }

	public static Message Create(Guid roomId, Guid senderId, Guid reciverId, string content,string senderDisplayName,string  senderUsername)
	{
		var message = new Message(Guid.NewGuid(), roomId, senderId, reciverId, content)
		{
			SenderDisplayName = senderDisplayName,
			SenderUsername = senderUsername
		};

		return message;
	}

	public void  Update(Guid roomId, Guid senderId, Guid reciverId, string content, string senderDisplayName, string senderUsername)
	{
		RoomId = roomId;
		SenderId = senderId;
		ReciverId = reciverId;
		Content = content;
		SenderDisplayName = senderDisplayName;
		SenderUsername = senderUsername;
		
	}
}
