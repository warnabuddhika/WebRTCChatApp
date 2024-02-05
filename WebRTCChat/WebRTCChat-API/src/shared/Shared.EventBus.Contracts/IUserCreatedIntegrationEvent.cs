namespace Shared.EventBus.Contracts
{
    public interface IUserCreatedIntegrationEvent
    {
        public Guid UserId { get; }
        public string FullName { get; }      
        public DateTime CreatedDate { get; }
    }
}
