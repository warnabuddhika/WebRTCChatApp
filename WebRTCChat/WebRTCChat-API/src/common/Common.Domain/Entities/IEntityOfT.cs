namespace Common.Domain.Entities;

    public interface IEntity : IRoot
    {

    }

    public interface IEntity<T> : IEntity where T : struct
    {
        T Id { get; }
    }
