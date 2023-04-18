using GamingApi.WebApi.Core.Interfaces.Services;
using Yld.GamingApi.WebApi.ApiContracts;

namespace GamingApi.WebApi.Core.Services
{
    public sealed class GameService : IGameService<GamesResponse>
    {
        public Task<GamesResponse> GetPaginatedGamesAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
