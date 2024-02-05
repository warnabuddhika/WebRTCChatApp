namespace UserManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class Room
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int RoomId { get; set; }
	public string RoomName { get; set; }
	public int CountMember { get; set; }

	public AppUser AppUser { get; set; }
	public Guid UserId { get; set; }

}


