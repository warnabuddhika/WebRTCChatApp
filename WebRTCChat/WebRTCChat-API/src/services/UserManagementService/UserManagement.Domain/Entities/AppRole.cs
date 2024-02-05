namespace UserManagement.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppRole : IdentityRole<Guid>
{
	public ICollection<AppUserRole> UserRoles { get; set; }
}
