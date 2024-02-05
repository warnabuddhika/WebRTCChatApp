namespace Common.Domain.Entities;

    public interface IAggregateRoot<T> : IEntity<T>, IAggregateRoot where T : struct
    {
    }
