
namespace GamingApi.WebApi.Contracts.Interfaces
{
    /// <summary>
    /// A proxy wrapper to perform Json operations.
    ///
    /// </summary>
    public interface IProxyJsonClient
    {
        public TResult? DeserializeStream<TResult>(Stream jsonStream);
    }
}
