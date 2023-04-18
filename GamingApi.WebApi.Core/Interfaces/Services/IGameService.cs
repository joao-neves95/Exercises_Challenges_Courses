namespace GamingApi.WebApi.Core.Interfaces.Services
{
    public interface IGameService<TResponse>
    {
        public Task<TResponse> GetPaginatedGamesAsync(int offset, int limit);
    }
}
