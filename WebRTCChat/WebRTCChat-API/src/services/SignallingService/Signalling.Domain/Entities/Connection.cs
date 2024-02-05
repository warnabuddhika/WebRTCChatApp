namespace Signalling.Domain.Entities;

using Common.Domain.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

public class Connection : AuditableEntity<Guid>,ISoftDelete
{
	public Connection() { }
	public Connection(string connectionId, string userName)
	{
		ConnectionId = connectionId;
		UserName = userName;
	}
	[Key]
	public string ConnectionId { get; set; }
	public string UserName { get; set; }
	public bool IsDeleted { get; private set; }

}
