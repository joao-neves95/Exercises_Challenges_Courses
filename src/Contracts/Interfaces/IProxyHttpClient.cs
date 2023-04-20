
namespace GamingApi.WebApi.Contracts.Interfaces
{
    /// <summary>
    /// A proxy wrapper to perform Http operations.
    ///
    /// </summary>
    public interface IProxyHttpClient
    {
        public Task<TResponse?> GetAsync<TResponse>(string endpoint);
    }
}
