
using GamingApi.WebApi.Contracts.Interfaces;
using GamingApi.WebApi.Contracts.Interfaces.Stores;
using GamingApi.WebApi.Infrastructure.Entities;

using Yld.GamingApi.WebApi.Core.Extensions;

namespace GamingApi.WebApi.Infrastructure.Network
{
    public sealed class GameStoreAwsClient : IGamesStore<DataGame>
    {
        private readonly IProxyHttpClient _httpClient;

        public GameStoreAwsClient(IProxyHttpClient httpClient)
        {
            _httpClient = httpClient.ThrowIfNull();
        }

        public Task<IEnumerable<DataGame>> GetAllGamesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
