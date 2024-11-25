using System.Runtime.Serialization;

namespace bootcamp_framework.Domain.Persistence
{
    [Serializable]
    public class ForeingKeyException : Exception
    {
        public ForeingKeyException()
        {
        }

        public ForeingKeyException(string? message) : base(message)
        {
        }

        public ForeingKeyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ForeingKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
