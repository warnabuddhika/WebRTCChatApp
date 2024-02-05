using Common.Domain.Events;

namespace Common.Domain.Entities;

    public interface IAggregateRoot : IRoot
    {
        IEnumerable<IDomainEvent> GetDomainEvents();
        void ClearDomainEvents();
    }
