
using Yld.GamingApi.WebApi.ApiContracts;

namespace GamingApi.WebApi.Core.Interfaces
{
    public interface IGameService
    {
        public Task<GamesResponse> GetPaginatedGamesAsync(int offset, int limit);
    }
}
