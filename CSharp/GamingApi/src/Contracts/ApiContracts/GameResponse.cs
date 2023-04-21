
namespace Yld.GamingApi.WebApi.ApiContracts
{
    public class GameResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Publisher { get; set; }

        public string Genre { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public PlatformsResponse Platforms { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int RequiredAge { get; set; }
    }
}
