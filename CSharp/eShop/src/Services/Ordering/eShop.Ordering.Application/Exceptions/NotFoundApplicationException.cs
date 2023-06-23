
namespace eShop.Ordering.Application.Exceptions
{
    public class NotFoundApplicationException : ApplicationException
    {
        public NotFoundApplicationException()
            : base()
        {
        }

        public NotFoundApplicationException(string name, object key)
            : base(BuildMessage(name, key))
        {
        }

        public NotFoundApplicationException(string name, object key, Exception? innerException)
            : base(BuildMessage(name, key), innerException)
        {
        }

        private static string BuildMessage(string name, object key) => $"Entity '{name}' '{key}' was not found.";
    }
}
