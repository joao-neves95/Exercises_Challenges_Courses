
using GamingApi.WebApi.Core.Interfaces.Stores;
using GamingApi.WebApi.Infrastructure.Entities;

namespace GamingApi.WebApi.Infrastructure.Network
{
    public sealed class GameStoreAwsClient : IGamesStore<DataGame>
    {
        public Task<IEnumerable<DataGame>> GetAllGamesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
