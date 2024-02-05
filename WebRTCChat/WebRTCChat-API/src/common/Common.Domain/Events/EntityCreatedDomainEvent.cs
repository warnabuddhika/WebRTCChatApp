using Common.Domain.Entities;

namespace Common.Domain.Events
{
    public class EntityCreatedDomainEvent<TAggregate> : IDomainEvent where TAggregate : IAggregateRoot
    {
        public TAggregate Entity { get; init; }

        public DateTime EvenTime { get; init; }

        public EntityCreatedDomainEvent(TAggregate entity, DateTime evenTime)
        {
            Entity = entity;
            EvenTime = evenTime;
        }
    }
}
