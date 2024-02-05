using System.Runtime.Serialization;

namespace Common.Domain.Exceptions
{
    [Serializable]
    public class EntityStateConflictException : Exception
    {
        public Type? EntityType { get; set; }

        public EntityStateConflictException()
        {
        }

        public EntityStateConflictException(Type entityType, string message) : base($"{message} Type:{entityType.FullName}")
        {
            EntityType = entityType;
        }

        public EntityStateConflictException(string? message) : base(message)
        {
        }

        public EntityStateConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityStateConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
