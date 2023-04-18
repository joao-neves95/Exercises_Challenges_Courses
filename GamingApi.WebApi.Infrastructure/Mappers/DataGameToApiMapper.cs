
using GamingApi.WebApi.Core.Interfaces;
using GamingApi.WebApi.Infrastructure.Entities;

using Yld.GamingApi.WebApi.ApiContracts;

namespace GamingApi.WebApi.Infrastructure.Mappers
{
    public sealed class DataGameToApiMapper : IMapper<DataGame, GameResponse>
    {
        public GameResponse Map(DataGame source)
        {
            return new GameResponse()
            {
                Id = source.AppId,
                Name = source.Name,
                ShortDescription = source.ShortDescription,
                Genre = source.Genre,
                Publisher = source.Publisher,
                ReleaseDate = source.ReleaseDate,
                RequiredAge = source.RequiredAge,
                Categories = source.Categories,
                Platforms = new PlatformsResponse()
                {
                    Linux = source.Platforms.Linux,
                    Mac = source.Platforms.Mac,
                    Windows = source.Platforms.Windows,
                },
            };
        }
    }
}
