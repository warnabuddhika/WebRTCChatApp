namespace Messaging.Domain.Entities;

using Common.Domain.Auditing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RoomUser : AuditableEntity<Guid>, ISoftDelete
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid RoomId { get; set; }

	public bool IsDeleted { get; private set; }
}
