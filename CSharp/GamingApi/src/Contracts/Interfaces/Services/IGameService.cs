
namespace GamingApi.WebApi.Contracts.Interfaces.Services
{
    public interface IGameService<TResponse>
        where TResponse : IApiResponse
    {
        public Task<TResponse> GetPaginatedGamesAsync(int offset, int limit);
    }
}
