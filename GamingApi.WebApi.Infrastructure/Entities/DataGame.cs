using Newtonsoft.Json;

namespace GamingApi.WebApi.Infrastructure.Entities
{
    public class DataGame
    {
        [JsonProperty("appid")]
        public int AppId { get; set; }

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public Dictionary<string, int> Tags { get; set; }
        public string Type { get; set; }
        public List<string> Categories { get; set; }
        public string Owners { get; set; }
        public int Positive { get; set; }
        public int Negative { get; set; }
        public string Price { get; set; }
        public string InitialPrice { get; set; }
        public string Discount { get; set; }

        [JsonProperty("ccu")]
        public int Ccu { get; set; }

        public string Languages { get; set; }
        public DataPlatforms Platforms { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RequiredAge { get; set; }
        public string Website { get; set; }
        public string HeaderImage { get; set; }
    }
}
