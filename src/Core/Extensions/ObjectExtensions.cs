
namespace Yld.GamingApi.WebApi.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T ThrowIfNull<T>(this T? @object)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(typeof(T).Name);
            }

            return @object;
        }
    }
}
