namespace Common.EventBus.Shared
{
    public interface ISharedEventBus
    {
        Task Publish<T>(T integrationEvent, CancellationToken cancellationToken = default);
    }
}
