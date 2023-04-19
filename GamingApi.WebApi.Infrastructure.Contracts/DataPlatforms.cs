using GamingApi.WebApi.Contracts.Interfaces;

namespace GamingApi.WebApi.Infrastructure.Entities
{
    public class DataPlatforms : IEntity
    {
        public bool Windows { get; set; }
        public bool Mac { get; set; }
        public bool Linux { get; set; }
    }
}
