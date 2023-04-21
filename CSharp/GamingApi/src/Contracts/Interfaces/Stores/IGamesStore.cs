
namespace GamingApi.WebApi.Contracts.Interfaces.Stores
{
    public interface IGamesStore<TResponse>
        where TResponse : IEntity
    {
        public Task<IEnumerable<TResponse>?> GetAllGamesAsync();
    }
}
