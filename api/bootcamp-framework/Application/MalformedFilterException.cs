using System.Runtime.Serialization;

namespace bootcamp_framework.Application
{
    [Serializable]
    public class MalformedFilterException : Exception
    {
        public MalformedFilterException()
        {
        }

        public MalformedFilterException(string? message) : base(message)
        {
        }

        public MalformedFilterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MalformedFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}