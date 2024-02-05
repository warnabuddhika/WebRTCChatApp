using Common.Domain.Events;
using MediatR;

namespace Common.EventBus.Local
{
    public class LocalEventBus : ILocalEventBus
    {
        private readonly IMediator _mediator;

        public LocalEventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
        }
    }
}
