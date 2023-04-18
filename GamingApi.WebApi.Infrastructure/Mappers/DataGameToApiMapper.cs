
using GamingApi.WebApi.Core.Interfaces;
using GamingApi.WebApi.Infrastructure.Entities;

using Yld.GamingApi.WebApi.ApiContracts;

namespace GamingApi.WebApi.Infrastructure.Mappers
{
    public sealed class DataGameToApiMapper : IMapper<DataGame, GameResponse>
    {
        public GameResponse Map(DataGame source)
        {
            throw new NotImplementedException();
        }
    }
}
