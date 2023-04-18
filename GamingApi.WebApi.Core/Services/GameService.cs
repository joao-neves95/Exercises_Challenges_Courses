
using GamingApi.WebApi.Core.Interfaces;

using Yld.GamingApi.WebApi.ApiContracts;

namespace GamingApi.WebApi.Core.Services
{
    internal sealed class GameService : IGameService
    {
        public Task<GamesResponse> GetPaginatedGamesAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
