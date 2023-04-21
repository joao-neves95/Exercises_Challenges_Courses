using GamingApi.WebApi.Contracts.Interfaces;

using Newtonsoft.Json;

namespace GamingApi.WebApi.Infrastructure.Entities
{
    public class DataGame : IEntity
    {
        [JsonProperty("appid")]
        public int AppId { get; set; }

        public string Name { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        public string Developer { get; set; }

        public string Publisher { get; set; }

        public string Genre { get; set; }

        public Dictionary<string, int> Tags { get; set; }

        public string Type { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public string Owners { get; set; }

        public int Positive { get; set; }

        public int Negative { get; set; }

        public string Price { get; set; }

        [JsonProperty("initial_price")]
        public string InitialPrice { get; set; }

        public string Discount { get; set; }

        [JsonProperty("ccu")]
        public int Ccu { get; set; }

        public string Languages { get; set; }

        public DataPlatforms Platforms { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("required_age")]
        public string RequiredAge { get; set; }

        public string Website { get; set; }

        [JsonProperty("header_image")]
        public string HeaderImage { get; set; }
    }
}
