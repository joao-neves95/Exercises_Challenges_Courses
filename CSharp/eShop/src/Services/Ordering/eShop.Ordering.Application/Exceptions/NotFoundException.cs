
namespace eShop.Ordering.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException()
            : base()
        {
        }

        public NotFoundException(string name, object key)
            : base(BuildMessage(name, key))
        {
        }

        public NotFoundException(string name, object key, Exception? innerException)
            : base(BuildMessage(name, key), innerException)
        {
        }

        private static string BuildMessage(string name, object key) => $"Entity '{name}' '{key}' was not found.";
    }
}
