
namespace GamingApi.WebApi.Core.Stores
{
    public interface IGamesStore
    {
        // TODO: Type the structure.
        public Task<IEnumerable<string>> GetAllGames();
    }
}
