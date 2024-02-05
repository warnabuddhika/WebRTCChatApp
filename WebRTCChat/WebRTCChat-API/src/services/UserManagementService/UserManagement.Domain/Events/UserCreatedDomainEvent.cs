using Common.Domain.Events;

namespace Users.Domain.Events
{
    public class UserCreatedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; init; }
        public Guid RegistrationId { get; init; }

    }
}
