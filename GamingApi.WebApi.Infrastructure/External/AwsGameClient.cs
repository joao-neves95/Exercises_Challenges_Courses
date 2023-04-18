
using GamingApi.WebApi.Core.Stores;

namespace GamingApi.WebApi.Infrastructure.Network
{
    public class AwsGameClient : IGamesStore
    {
        public Task<IEnumerable<string>> GetAllGames()
        {
            throw new NotImplementedException();
        }
    }
}
