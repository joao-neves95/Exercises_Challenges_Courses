
using GamingApi.WebApi.Contracts.Config;
using GamingApi.WebApi.Contracts.Interfaces;

using Microsoft.Extensions.Options;

using Polly;

using Yld.GamingApi.WebApi.Core.Extensions;

namespace GamingApi.WebApi.Infrastructure
{
    public sealed class DotnetHttpClient : IProxyHttpClient
    {
        private readonly IOptions<AppConfig> _appConfig;

        private readonly HttpClient _httpClient;

        private readonly IProxyJsonClient _jsonClient;

        public DotnetHttpClient(
            IOptions<AppConfig> appConfig,
            HttpClient httpClient,
            IProxyJsonClient jsonClient)
        {
            _appConfig = appConfig.ThrowIfNull();
            _httpClient = httpClient.ThrowIfNull();
            _jsonClient = jsonClient.ThrowIfNull();
        }

        public async Task<TResponse?> GetAsync<TResponse>(string endpoint)
        {
            if (endpoint is null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            var pollyResponse = await Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(GetSleepDurationPolicy(_appConfig.Value.NumberOfHttpRetries))
                .ExecuteAndCaptureAsync(async () => await _httpClient.GetStreamAsync(endpoint));

            if (pollyResponse.FinalException is not null)
            {
                throw pollyResponse.FinalException;
            }

            var result = _jsonClient.DeserializeStream<TResponse?>(pollyResponse.Result);
            await pollyResponse.Result.DisposeAsync();

            return result;
        }

        private static IEnumerable<TimeSpan> GetSleepDurationPolicy(int numberOfRetries)
        {
            for (var i = 1; i <= numberOfRetries; ++i)
            {
                yield return TimeSpan.FromSeconds(i);
            }
        }
    }
}
