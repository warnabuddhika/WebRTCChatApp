using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Common.Domain.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public Type? EntityType { get; set; }

        public object? Id { get; set; }

        public EntityNotFoundException(Type type)
        {
        }

        public EntityNotFoundException([NotNull] Type entityType, object id) : base($"Entity not found. Type:{entityType.FullName} Id:{id}")
        {
            EntityType = entityType;
            Id = id;
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }


        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
