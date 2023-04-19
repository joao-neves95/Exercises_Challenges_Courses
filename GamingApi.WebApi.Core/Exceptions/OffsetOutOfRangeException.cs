
namespace GamingApi.WebApi.Core.Exceptions
{
    public class OffsetOutOfRangeException : ArgumentOutOfRangeException
    {
        public OffsetOutOfRangeException() : base()
        {
        }

        public OffsetOutOfRangeException(string? paramName) : base(paramName)
        {
        }

        public OffsetOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public OffsetOutOfRangeException(string? paramName, object? actualValue, string? message) : base(paramName, actualValue, message)
        {
        }

        public OffsetOutOfRangeException(string? paramName, string? message) : base(paramName, message)
        {
        }
    }
}
