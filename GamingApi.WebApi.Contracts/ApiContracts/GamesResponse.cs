
using GamingApi.WebApi.Contracts.Interfaces;

namespace Yld.GamingApi.WebApi.ApiContracts
{
    public class GamesResponse : IApiResponse
    {
        public IEnumerable<GameResponse> Items { get; set; } = Enumerable.Empty<GameResponse>();

        public int? TotalItems => Items?.Count();
    }
}
