namespace Common.EventBus.Shared
{
    public class DummySharedEventBus : ISharedEventBus
    {
        public Task Publish<T>(T integrationEvent, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
