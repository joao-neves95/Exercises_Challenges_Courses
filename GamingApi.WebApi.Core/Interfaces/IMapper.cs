
namespace GamingApi.WebApi.Core.Interfaces
{
    /// <summary>
    /// A class to map data from TSource to TDestination
    ///
    /// </summary>
    public interface IMapper<in TSource, out TDestination>
    {
        public TDestination Map(TSource source);
    }
}
