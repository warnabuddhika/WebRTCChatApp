namespace Messaging.Domain.Entities;

using Common.Domain.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
