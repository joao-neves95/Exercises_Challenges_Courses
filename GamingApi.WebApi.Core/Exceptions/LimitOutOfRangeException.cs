
namespace GamingApi.WebApi.Core.Exceptions
{
    public class LimitOutOfRangeException : ArgumentOutOfRangeException
    {
        public LimitOutOfRangeException() : base()
        {
        }

        public LimitOutOfRangeException(string? paramName) : base(paramName)
        {
        }

        public LimitOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public LimitOutOfRangeException(string? paramName, object? actualValue, string? message) : base(paramName, actualValue, message)
        {
        }

        public LimitOutOfRangeException(string? paramName, string? message) : base(paramName, message)
        {
        }
    }
}
