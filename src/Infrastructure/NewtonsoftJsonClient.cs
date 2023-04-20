using GamingApi.WebApi.Contracts.Interfaces;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GamingApi.WebApi.Infrastructure
{
    /// <summary>
    /// A proxy wrapper to perform Json operations.
    ///
    /// </summary>
    public class NewtonsoftJsonClient : IProxyJsonClient
    {
        public TResult? DeserializeStream<TResult>(Stream jsonStream)
        {
            if (jsonStream is null)
            {
                return default;
            }

            using var sr = new StreamReader(jsonStream);
            using JsonReader reader = new JsonTextReader(sr);

            return new JsonSerializer().Deserialize<TResult>(reader);
        }
    }
}
