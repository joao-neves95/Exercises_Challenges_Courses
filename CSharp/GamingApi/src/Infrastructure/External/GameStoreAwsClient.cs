
using GamingApi.WebApi.Contracts.Config;
using GamingApi.WebApi.Contracts.Interfaces;
using GamingApi.WebApi.Contracts.Interfaces.Stores;
using GamingApi.WebApi.Infrastructure.Entities;

using Microsoft.Extensions.Options;

using Yld.GamingApi.WebApi.Core.Extensions;

namespace GamingApi.WebApi.Infrastructure.Network
{
    public sealed class GameStoreAwsClient : IGamesStore<DataGame>
    {
        private readonly IOptions<YldConfig> _yldConfig;

        private readonly IProxyHttpClient _httpClient;

        public GameStoreAwsClient(IOptions<YldConfig> yldConfig, IProxyHttpClient httpClient)
        {
            _yldConfig = yldConfig.ThrowIfNull();
            _httpClient = httpClient.ThrowIfNull();
        }

        public async Task<IEnumerable<DataGame>?> GetAllGamesAsync()
        {
            return await _httpClient.GetAsync<IEnumerable<DataGame>?>(_yldConfig.Value?.SteamGamesUrl!);
        }
    }
}
