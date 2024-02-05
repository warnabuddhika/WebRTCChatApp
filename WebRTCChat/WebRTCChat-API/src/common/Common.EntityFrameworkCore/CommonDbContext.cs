using Common.Domain.Auditing;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.Repositories;
using Common.EventBus.Local;
using Common.Security.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics.CodeAnalysis;

namespace Common.EntityFrameworkCore;

public class CommonDbContext : DbContext, IUnitOfWork
    {
        private ICurrentUser? _currentUser;       
        private ILocalEventBus? _localEventBus;

        private static Type EventType = typeof(EntityCreatedDomainEvent<>);

        public ICurrentUser CurrentUser
        {
            get
            {
                _currentUser ??= this.GetService<ICurrentUser>();

                return _currentUser;
            }
        }

    

        public ILocalEventBus LocalEventBus
        {
            get
            {
                _localEventBus ??= this.GetService<ILocalEventBus>();

                return _localEventBus;
            }
        }

        public string Schema { get; protected set; } = "dbo";

        public CommonDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating([NotNull] ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypes = modelBuilder.Model.GetEntityTypes();

            var methodInfo = typeof(CommonDbContext)
                .GetMethod(nameof(ConfigureBaseProperties), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            foreach (var entityType in entityTypes)
            {
                methodInfo?.MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        protected virtual void ConfigureBaseProperties<TEntity>([NotNull] ModelBuilder modelBuilder, [NotNull] IMutableEntityType mutableEntityType)
        where TEntity : class
        {
            if (mutableEntityType.IsOwned())
            {
                return;
            }

            if (!typeof(IEntity).IsAssignableFrom(typeof(TEntity)))
            {
                return;
            }

            modelBuilder.Entity<TEntity>().ConfigureModels();
        }

        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntries();
            UpdateSoftDeletedEntities();  

            var events = GetAllDomainEvents();

            var result = await SaveChangesAsync(cancellationToken);

            await PublishEvents(events, cancellationToken);

            return result;
        }

        protected virtual IEnumerable<IDomainEvent> GetAllDomainEvents()
        {
            var allEvents = new List<IDomainEvent>();

            IEnumerable<EntityEntry<IAggregateRoot>> entries = ChangeTracker.Entries<IAggregateRoot>();

            foreach (EntityEntry<IAggregateRoot> entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    var constructed = EventType.MakeGenericType(entry.Metadata.ClrType);
                    object? o = Activator.CreateInstance(constructed, entry.Entity, DateTime.UtcNow);
                    if (o != null)
                    {
                        allEvents.Add((IDomainEvent)o);
                    }

                }

                var events = entry.Entity.GetDomainEvents();
                entry.Entity.ClearDomainEvents();
                allEvents.AddRange(events);

            }

            return allEvents;
        }

        protected virtual async Task PublishEvents([NotNull] IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var @event in domainEvents)
            {
                await LocalEventBus.Publish(@event, cancellationToken);
            }
        }

        protected virtual void UpdateSoftDeletedEntities()
        {
            IEnumerable<EntityEntry<ISoftDelete>> entries = ChangeTracker.Entries<ISoftDelete>()
                .Where(e => e.State == EntityState.Deleted);

            foreach (EntityEntry<ISoftDelete> entry in entries)
            {
                entry.State = EntityState.Modified;
                entry.Property(a => a.IsDeleted).CurrentValue = true;
            }

        }

        protected virtual void UpdateAuditableEntries()
        {
            IEnumerable<EntityEntry<IAudtiableEntity>> entries = ChangeTracker
                .Entries<IAudtiableEntity>();

            foreach (EntityEntry<IAudtiableEntity> entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(a => a.CreatedTime).CurrentValue = DateTime.UtcNow;
                    entry.Property(a => a.UpdatedTime).CurrentValue = DateTime.UtcNow;

                    if (CurrentUser.Id.HasValue)
                    {
                        entry.Property(a => a.CreatedById).CurrentValue = CurrentUser.Id.Value;
                        entry.Property(a => a.UpdatedById).CurrentValue = CurrentUser.Id.Value;
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(a => a.UpdatedTime).CurrentValue = DateTime.UtcNow;

                    if (CurrentUser.Id.HasValue)
                    {
                        entry.Property(a => a.UpdatedById).CurrentValue = CurrentUser.Id.Value;
                    }
                }

            }
        }
    }
