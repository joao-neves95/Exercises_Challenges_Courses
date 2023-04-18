
namespace GamingApi.WebApi.Core.Stores
{
    public interface IGamesStore<TResponse>
    {
        public Task<IEnumerable<TResponse>> GetAllGames();
    }
}
