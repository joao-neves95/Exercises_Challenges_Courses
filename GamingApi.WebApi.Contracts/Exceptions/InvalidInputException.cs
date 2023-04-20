
namespace GamingApi.WebApi.Contracts.Exceptions
{
    public class InvalidInputException : ArgumentException
    {
        public InvalidInputException() : base()
        {
        }

        public InvalidInputException(string? message) : base(message)
        {
        }

        public InvalidInputException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public InvalidInputException(string? message, string? paramName) : base(message, paramName)
        {
        }

        public InvalidInputException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
        {
        }
    }
}
