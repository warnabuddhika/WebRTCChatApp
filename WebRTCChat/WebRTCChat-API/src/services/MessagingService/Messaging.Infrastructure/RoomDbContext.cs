using Common.EntityFrameworkCore;
using Messaging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Messaging.Infrastructure;

public class RoomDbContext : CommonDbContext
    {
        public RoomDbContext(DbContextOptions<RoomDbContext> options)
        : base(options)
        {
            Schema = "msg";
        }
       
        public virtual DbSet<Room> Rooms { get; set; }
		public DbSet<Connection> Connections { get; set; }
		public DbSet<Message> Messages { get; set; }	



	protected override void OnModelCreating([NotNull] ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Room>(b =>
            {
                b.ToTable(nameof(Rooms), Schema);

			b.Property(e => e.Id)
			.IsRequired();                                  
               
            });

        }
    }
