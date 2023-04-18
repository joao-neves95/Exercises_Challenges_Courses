
using GamingApi.WebApi.Core.Stores;

namespace GamingApi.WebApi.Infrastructure.Network
{
    public class AwsGameClient<TResponse> : IGamesStore<TResponse>
    {
        public Task<IEnumerable<TResponse>> GetAllGames()
        {
            throw new NotImplementedException();
        }
    }
}
