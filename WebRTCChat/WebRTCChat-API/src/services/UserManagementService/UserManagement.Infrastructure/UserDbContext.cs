using Common.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using UserManagement.Domain.Entities;
using UserManagement.Application.Features.Applicants.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Emit;

namespace UserManagement.Infrastructure;

public class UserDbContext : IdentityDbContext<AppUser, AppRole, Guid,
		IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>,
		IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
	public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

	public DbSet<Room> Rooms { get; set; }	

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<AppUser>()
			.HasMany(ur => ur.UserRoles)
			.WithOne(u => u.User)
			.HasForeignKey(ur => ur.UserId)
			.IsRequired();

		builder.Entity<AppRole>()
			.HasMany(ur => ur.UserRoles)
			.WithOne(u => u.Role)
			.HasForeignKey(ur => ur.RoleId)
			.IsRequired();

		//Tbl User and Room: 1 user has many room (1-n)
		builder.Entity<Room>()
		.HasOne(s => s.AppUser)
		.WithMany(g => g.Rooms)
		.HasForeignKey(s => s.UserId);
		
	}
}

