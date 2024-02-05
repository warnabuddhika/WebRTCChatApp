namespace Common.Domain.Entities;

    public abstract class Entity<T> : IEntity<T> where T : struct
    {
        public virtual T Id { get; protected set; }

        protected Entity()
        {

        }

        protected Entity(T id)
        {
            Id = id;
        }
    }
