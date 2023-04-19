
using GamingApi.WebApi.Contracts.Interfaces;

using Yld.GamingApi.WebApi.Core.Extensions;

namespace GamingApi.WebApi.Infrastructure
{
    public sealed class DotnetHttpClient : IProxyHttpClient
    {
        private readonly HttpClient _httpClient;

        private readonly IProxyJsonClient _jsonClient;

        public DotnetHttpClient(
            HttpClient httpClient,
            IProxyJsonClient jsonClient)
        {
            _httpClient = httpClient.ThrowIfNull();
            _jsonClient = jsonClient.ThrowIfNull();
        }

        public async Task<TResponse?> GetAsync<TResponse>(string endpoint)
        {
            if (endpoint is null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            using var jsonStream = await _httpClient.GetStreamAsync(endpoint);

            return _jsonClient.DeserializeStream<TResponse?>(jsonStream);
        }
    }
}
