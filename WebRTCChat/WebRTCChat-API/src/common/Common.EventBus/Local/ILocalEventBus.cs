using Common.Domain.Events;

namespace Common.EventBus.Local
{
    public interface ILocalEventBus
    {

        Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
