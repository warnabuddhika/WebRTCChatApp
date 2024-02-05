namespace Signalling.Domain.Entities;

using Common.Domain.Auditing;
using System;

public class RoomUser : AuditableEntity<Guid>, ISoftDelete
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid RoomId { get; set; }

	public bool IsDeleted { get; private set; }
}
