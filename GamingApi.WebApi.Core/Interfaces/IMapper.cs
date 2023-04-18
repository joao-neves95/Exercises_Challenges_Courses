
namespace GamingApi.WebApi.Core.Interfaces
{
    // We use hand-made mappers, instead something like AutoMapper because they are faster, more flexible and "strongly typed".
    /// <summary>
    /// A class to map data from TSource to TDestination
    ///
    /// </summary>
    public interface IMapper<in TSource, out TDestination>
    {
        public TDestination Map(TSource source);
    }
}
