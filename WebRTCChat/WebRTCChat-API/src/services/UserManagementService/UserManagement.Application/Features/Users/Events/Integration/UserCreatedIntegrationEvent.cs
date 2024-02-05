using Shared.EventBus.Contracts;

namespace UserManagement.Application.Features.Users.Events.Integration;

    public class UserCreatedIntegrationEvent : IUserCreatedIntegrationEvent
    {  

	public Guid UserId { get; init; }

	public string FullName { get; init; }

	public DateTime CreatedDate { get; init; }
}
