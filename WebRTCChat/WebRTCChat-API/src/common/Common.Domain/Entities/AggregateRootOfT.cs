using Common.Domain.Events;

namespace Common.Domain.Entities;

    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T> where T : struct
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        protected AggregateRoot() { }

        protected AggregateRoot(T id) : base(id) { }

        public virtual void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public virtual IEnumerable<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents;
        }

        protected virtual void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        protected virtual void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
    }
