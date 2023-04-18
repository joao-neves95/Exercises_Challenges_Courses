namespace GamingApi.WebApi.Core.Interfaces.Stores
{
    public interface IGamesStore<TResponse>
    {
        public Task<IEnumerable<TResponse>> GetAllGamesAsync();
    }
}
