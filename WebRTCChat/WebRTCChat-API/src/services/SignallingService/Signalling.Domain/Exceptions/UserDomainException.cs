
using System.Runtime.Serialization;

namespace Users.Domain.Exceptions
{
    [Serializable]
    public class UserDomainException : Exception
    {
        public UserDomainException()
        {
        }

        public UserDomainException(string? message) : base(message)
        {
        }

        public UserDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
