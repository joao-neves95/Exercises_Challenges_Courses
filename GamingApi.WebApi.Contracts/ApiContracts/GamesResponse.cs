
namespace Yld.GamingApi.WebApi.ApiContracts
{
    public class GamesResponse
    {
        public IEnumerable<GameResponse> Items { get; set; } = Enumerable.Empty<GameResponse>();

        public int? TotalItems => Items?.Count();
    }
}
