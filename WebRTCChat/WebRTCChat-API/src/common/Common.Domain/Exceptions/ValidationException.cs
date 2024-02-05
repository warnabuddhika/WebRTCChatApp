using System.Runtime.Serialization;

namespace Common.Domain.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public string? Parmaeter { get; }

        public ValidationException()
        {

        }

        public ValidationException(string? message) : base(message)
        {
        }

        public ValidationException(string parmaeter, string? message) : this(message)
        {
            Parmaeter = parmaeter;
        }

        public ValidationException(string parmaeter, string? message, Exception? innerException) : base(message, innerException)
        {
            Parmaeter = parmaeter;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
