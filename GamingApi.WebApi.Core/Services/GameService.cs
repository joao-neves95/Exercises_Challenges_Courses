using GamingApi.WebApi.Contracts.Interfaces.Services;
using GamingApi.WebApi.Contracts.Interfaces.Stores;
using GamingApi.WebApi.Infrastructure.Entities;

using Yld.GamingApi.WebApi.ApiContracts;
using Yld.GamingApi.WebApi.Core.Extensions;

namespace GamingApi.WebApi.Core.Services
{
    public sealed class GameService : IGameService<GamesResponse>
    {
        private readonly IGamesStore<DataGame> _gamesStore;

        public GameService(IGamesStore<DataGame> gamesStore)
        {
            _gamesStore = gamesStore.ThrowIfNull();
        }

        public Task<GamesResponse> GetPaginatedGamesAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
